using Domain.Dto.Search;
using Domain.Entities;
using Domain.Models.Search;
using Domain.Repositories;
using Domain.Services;
using Nest;

namespace Infrastructure.Services;

public class SearchService : ISearchService
{
    private const string IndexName = "card-index";
    private readonly IElasticClient _elasticClient;
    private readonly ICardRepository _cardRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IAttributeRepository _attributeRepository;

    public SearchService(IElasticClient elasticClient, ICardRepository cardRepository, ICategoryRepository categoryRepository, IAttributeRepository attributeRepository)
    {
        _elasticClient = elasticClient;
        _cardRepository = cardRepository;
        _categoryRepository = categoryRepository;
        _attributeRepository = attributeRepository;
    }

    public async Task<bool> CreateIndexAsync(CancellationToken cancellationToken = default)
    {
        var isExist = await _elasticClient.Indices.ExistsAsync(new IndexExistsRequest(IndexName), cancellationToken);
        if (isExist.Exists)
        {
            return true;
        }

        var indexResponse = await _elasticClient.Indices.CreateAsync(IndexName, descriptor => descriptor.Map<CardIndexModel>(q => q.AutoMap()), cancellationToken);
        return indexResponse.IsValid;
    }

    public async Task<bool> DeleteIndexAsync(CancellationToken cancellationToken = default)
    {
        var indexResponse = await _elasticClient.Indices.DeleteAsync(new DeleteIndexRequest(IndexName), cancellationToken);
        return indexResponse.IsValid;
    }

    public async Task<bool> IndexCardAsync(string cardId, CancellationToken cancellationToken = default)
    {
        var card = await _cardRepository.GetCardAsync(cardId, cancellationToken);
        if (card == null)
            return true;

        var cardIndexModel = new CardIndexModel
        {
            CategoryId = card.CategoryId,
            Id = card.Id,
            Attributes = new CardIndexModel.CardIndexAttributeModel
            {
                BooleanValues = MapBoolValues(card.Attributes),
                DateValues = MapDateValues(card.Attributes),
                StringValues = MapStringValues(card.Attributes),
                LocationValues = MapLocationValues(card.Attributes),
                NumericValues = MapNumericValues(card.Attributes),
                SelectedItems = MapSelectedItems(card.Attributes),
                DateRangeValues = MapDateRangeValues(card.Attributes),
                NumericRangeValues = MapNumericRangeValues(card.Attributes)
            }
        };

        var indexResponse = await _elasticClient.IndexAsync(new IndexRequest<CardIndexModel>(cardIndexModel, IndexName, cardIndexModel.Id), cancellationToken);
        return indexResponse.IsValid;
    }

    public async Task<SearchResponseModel> Search(SearchRequestModel requestModel, CancellationToken cancellationToken)
    {
        var categoryAttributeMappings = await _categoryRepository.GetCategoryAttributeMappingsAsync(requestModel.CategoryId, cancellationToken);
        var attributes = await _attributeRepository.GetAttributesByIdsAsync(categoryAttributeMappings.Select(x => x.AttributeId).ToList(), cancellationToken);


        var searchRequest = new SearchRequest(IndexName)
        {
            From = requestModel.From,
            Size = requestModel.Size
        };


        var filterAblesAttributes = categoryAttributeMappings.Where(q => q.IsFilterable).ToList();

        foreach (var filterAttribute in filterAblesAttributes)
        {
            var attribute = attributes.FirstOrDefault(q => q.Id == filterAttribute.AttributeId);
            if (attribute == null)
                continue;

            ConfigureAggregation(searchRequest, attribute);
        }

        var searchResponse = await _elasticClient.SearchAsync<CardIndexModel>(searchRequest, cancellationToken);
        return new SearchResponseModel
        {
            Cards = searchResponse.Hits.Select(q => q.Source).ToList(),
            Total = searchResponse.Total,
            Filters = new SearchResponseModel.FilterModel()
        };
    }

    private static List<CardIndexModel.NumericRangeAttributeItemModel> MapNumericRangeValues(List<CardEntity.CardAttributeMappingModel> cardAttributes)
    {
        var result = new List<CardIndexModel.NumericRangeAttributeItemModel>();
        foreach (var attribute in cardAttributes)
        {
            var numericRangeModels = attribute.Values.Where(q => q.NumericRangeValue != null).Select(q => q.NumericRangeValue).ToList();
            foreach (var numericRangeModel in numericRangeModels)
            {
                if (numericRangeModel is not {Min: not null} || !numericRangeModel.Max.HasValue)
                    continue;

                result.Add(new CardIndexModel.NumericRangeAttributeItemModel
                {
                    AttributeId = attribute.AttributeId,
                    Max = numericRangeModel.Max.Value,
                    Min = numericRangeModel.Min.Value
                });
            }
        }

        return result;
    }

    private static List<CardIndexModel.DateRangeAttributeItemModel> MapDateRangeValues(List<CardEntity.CardAttributeMappingModel> cardAttributes)
    {
        var result = new List<CardIndexModel.DateRangeAttributeItemModel>();
        foreach (var attribute in cardAttributes)
        {
            var dateRangeModels = attribute.Values.Where(q => q.DateTimeRangeValue != null).Select(q => q.DateTimeRangeValue).ToList();
            foreach (var dateRangeModel in dateRangeModels)
            {
                if (dateRangeModel == null)
                    continue;

                result.Add(new CardIndexModel.DateRangeAttributeItemModel
                {
                    AttributeId = attribute.AttributeId,
                    Max = dateRangeModel.Max,
                    Min = dateRangeModel.Min
                });
            }
        }

        return result;
    }

    private static List<CardIndexModel.SelectedItemsAttributeItemModel> MapSelectedItems(List<CardEntity.CardAttributeMappingModel> cardAttributes)
    {
        var result = new List<CardIndexModel.SelectedItemsAttributeItemModel>();
        foreach (var attribute in cardAttributes)
        {
            var itemsModels = attribute.Values.Where(q => q.ItemIds != null).Select(q => q.ItemIds).ToList();
            foreach (var items in itemsModels)
            {
                if (items == null)
                    continue;

                foreach (var item in items)
                {
                    result.Add(new CardIndexModel.SelectedItemsAttributeItemModel
                    {
                        AttributeId = attribute.AttributeId,
                        Value = item
                    });
                }
            }
        }

        return result;
    }

    private static List<CardIndexModel.NumericAttributeItemModel> MapNumericValues(List<CardEntity.CardAttributeMappingModel> cardAttributes)
    {
        var result = new List<CardIndexModel.NumericAttributeItemModel>();
        foreach (var attribute in cardAttributes)
        {
            var numericModels = attribute.Values.Where(q => q.NumericValue != null).Select(q => q.NumericValue).ToList();
            foreach (var numericValue in numericModels)
            {
                if (!numericValue.HasValue)
                    continue;

                result.Add(new CardIndexModel.NumericAttributeItemModel
                {
                    AttributeId = attribute.AttributeId,
                    Value = numericValue.Value
                });
            }
        }

        return result;
    }

    private static List<CardIndexModel.LocationAttributeItemModel> MapLocationValues(List<CardEntity.CardAttributeMappingModel> cardAttributes)
    {
        var result = new List<CardIndexModel.LocationAttributeItemModel>();
        foreach (var attribute in cardAttributes)
        {
            var locationValues = attribute.Values.Where(q => q.LocationValue != null).Select(q => q.LocationValue).ToList();
            foreach (var location in locationValues)
            {
                if (location == null)
                    continue;

                result.Add(new CardIndexModel.LocationAttributeItemModel
                {
                    AttributeId = attribute.AttributeId,
                    Value = new GeoCoordinate(location.Lat, location.Lng)
                });
            }
        }

        return result;
    }

    private static List<CardIndexModel.StringAttributeItemModel> MapStringValues(List<CardEntity.CardAttributeMappingModel> cardAttributes)
    {
        var result = new List<CardIndexModel.StringAttributeItemModel>();
        foreach (var attribute in cardAttributes)
        {
            var stringValues = attribute.Values.Where(q => q.StringValue != null).Select(q => q.StringValue).ToList();
            foreach (var stringValue in stringValues)
            {
                if (stringValue == null)
                    continue;

                result.Add(new CardIndexModel.StringAttributeItemModel
                {
                    AttributeId = attribute.AttributeId,
                    Value = stringValue.FirstOrDefault().Label
                });
            }
        }

        return result;
    }

    private static List<CardIndexModel.DateAttributeItemModel> MapDateValues(List<CardEntity.CardAttributeMappingModel> cardAttributes)
    {
        var result = new List<CardIndexModel.DateAttributeItemModel>();
        foreach (var attribute in cardAttributes)
        {
            var dateTimes = attribute.Values.Where(q => q.DateTimeValue != null).Select(q => q.DateTimeValue).ToList();
            foreach (var dateTime in dateTimes)
            {
                if (!dateTime.HasValue)
                    continue;

                result.Add(new CardIndexModel.DateAttributeItemModel
                {
                    AttributeId = attribute.AttributeId,
                    Value = dateTime.Value
                });
            }
        }

        return result;
    }

    private static List<CardIndexModel.BooleanAttributeItemModel> MapBoolValues(List<CardEntity.CardAttributeMappingModel> cardAttributes)
    {
        var result = new List<CardIndexModel.BooleanAttributeItemModel>();
        foreach (var attribute in cardAttributes)
        {
            var boolValues = attribute.Values.Select(q => q.BoolValue).ToList();
            foreach (var boolValue in boolValues)
            {
                if (!boolValue.HasValue)
                    continue;
                result.Add(new CardIndexModel.BooleanAttributeItemModel
                {
                    AttributeId = attribute.AttributeId,
                    Value = boolValue.Value
                });
            }
        }

        return result;
    }

    private static void ConfigureAggregation(ISearchRequest searchRequest, AttributeEntity attribute)
    {
        searchRequest.Aggregations ??= new AggregationDictionary();
        switch (attribute.Type)
        {
            case AttributeType.String:
                return;
                break;
            case AttributeType.StringMultiLine:
                return;
                break;
            case AttributeType.Numeric:
                searchRequest.Aggregations.Add(attribute.Id + "-min", new NestedAggregation(attribute.Id)
                {
                    Path = "attributes.numericValues",
                    Aggregations = new AggregationDictionary
                    {
                        {attribute.Id + "-min", new MinAggregation(attribute.Id + "-min", new Field("attributes.numericValues.value"))}
                    }
                });
                searchRequest.Aggregations.Add(attribute.Id + "-max", new NestedAggregation(attribute.Id)
                {
                    Path = "attributes.numericValues",
                    Aggregations = new AggregationDictionary
                    {
                        {attribute.Id + "-max", new MinAggregation(attribute.Id + "-max", new Field("attributes.numericValues.value"))}
                    }
                });
                break;
            case AttributeType.NumericRange:
                break;
            case AttributeType.Boolean:
                searchRequest.Aggregations.Add(attribute.Id, new NestedAggregation(attribute.Id)
                {
                    Path = "attributes.booleanValues",
                    Aggregations = new AggregationDictionary
                    {
                        {"selectedValue", new TermsAggregation("value") {Field = new Field("attributes.booleanValues.value")}}
                    }
                });
                break;
            case AttributeType.ListSelect:
                searchRequest.Aggregations.Add(attribute.Id, new NestedAggregation(attribute.Id)
                {
                    Path = "attributes.selectedItems",
                    Aggregations = new AggregationDictionary
                    {
                        {"AggregationValue", new TermsAggregation("AggregationValue") {Field = new Field("attributes.selectedItems.value")}}
                    }
                });
                break;
            case AttributeType.Price:
                break;
            case AttributeType.PriceRange:
                break;
            case AttributeType.Date:
                searchRequest.Aggregations.Add(attribute.Id + "-min", new NestedAggregation(attribute.Id)
                {
                    Path = "attributes.dateValues",
                    Aggregations = new AggregationDictionary
                    {
                        {attribute.Id + "-min", new MinAggregation(attribute.Id + "-min", new Field("attributes.dateValues.value"))}
                    }
                });
                searchRequest.Aggregations.Add(attribute.Id + "-max", new NestedAggregation(attribute.Id)
                {
                    Path = "attributes.dateValues",
                    Aggregations = new AggregationDictionary
                    {
                        {attribute.Id + "-max", new MinAggregation(attribute.Id + "-max", new Field("attributes.dateValues.value"))}
                    }
                });
                break;
            case AttributeType.DateRange:
                break;
            case AttributeType.Location:
                return;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}