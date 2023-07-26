using Api.Infrastructure.Context;
using Api.Infrastructure.Contract;
using Domain.Dto;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Category.Card.Structure;

public class Get : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string categoryId,
        [FromServices] IApiContext apiContext,
        [FromServices] ICategoryRepository categoryRepository,
        [FromServices] IAttributeRepository attributeRepository,
        CancellationToken cancellationToken)
    {
        var categoryEntity = await categoryRepository.GetCategoryAsync(categoryId, cancellationToken);
        if (categoryEntity == null)
        {
            return Results.NotFound();
        }

        var categoryAttributeMappings = await categoryRepository.GetCategoryAttributeMappingsAsync(categoryId, cancellationToken);
        var attributes = await attributeRepository.GetAttributesByIdsAsync(categoryAttributeMappings.Select(x => x.AttributeId).ToList(), cancellationToken);

        //TODO
        const string defaultCulture = "tr-TR";
        var categoryTranslations = categoryEntity.Translations.FirstOrDefault(q => q.Culture == apiContext.CurrentCulture) ?? categoryEntity.Translations.First(q => q.Culture == defaultCulture);

        var response = new CardStructureDto
        {
            Category = new CategoryDto
            {
                Id = categoryEntity.Id,
                ImageUrl = categoryEntity.ImageUrl,
                Description = categoryTranslations.Description,
                Hint = categoryTranslations.Hint,
                Label = categoryTranslations.Label,
                Placeholder = categoryTranslations.Placeholder
            }
        };


        foreach (var categoryAttribute in categoryAttributeMappings)
        {
            var attribute = attributes.FirstOrDefault(q => q.Id == categoryAttribute.AttributeId);
            if (attribute == null)
            {
                continue;
            }

            var attributeTranslations = attribute.Translations.FirstOrDefault(q => q.Culture == apiContext.CurrentCulture) ?? attribute.Translations.First(q => q.Culture == defaultCulture);

            var attributeItems = new List<CardStructureDto.AttributeItemModel>();
            if (attribute.Items.Any())
            {
                foreach (var attributeItem in attribute.Items)
                {
                    attributeItems.Add(new CardStructureDto.AttributeItemModel
                    {
                        Id = attributeItem.Id,
                        ItemValue = attributeItem.ItemValue,
                        Icon = attributeItem.Icon
                    });
                }
            }

            response.Attributes.Add(new CardStructureDto.CardStructureAttributeItemDto
            {
                Id = attribute.Id,
                Type = attribute.Type,
                SystemName = attribute.SystemName,
                Description = attributeTranslations.Description,
                Hint = attributeTranslations.Hint,
                Label = attributeTranslations.Label,
                Placeholder = attributeTranslations.Placeholder,
                Items = attributeItems,
                DefaultValues = categoryAttribute.DefaultValues,
                Rank = categoryAttribute.Rank,
                IsRequired = categoryAttribute.IsRequired,
                IsSingleSelection = categoryAttribute.IsSingleSelection,
                
            });
        }


        response.Attributes = response.Attributes.OrderBy(q => q.Page).ThenBy(q => q.Rank).ToList();
        return Results.Ok(response);
    }


    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/category/{categoryId}/structure", Handler)
            .Produces<CardStructureDto>()
            .WithTags("Category");
    }
}