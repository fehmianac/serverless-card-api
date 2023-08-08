using Api.Infrastructure.Context;
using Api.Infrastructure.Contract;
using Domain.Dto;
using Domain.Mappers;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Card;

public class Get : IEndpoint
{
    private static async Task<IResult> Handler([FromRoute] string id,
        [FromServices] IApiContext apiContext,
        [FromServices] ICardRepository cardRepository,
        [FromServices] ICategoryRepository categoryRepository,
        [FromServices] IAttributeRepository attributeRepository,
        [FromServices] ISearchService searchService,
        CancellationToken cancellationToken)
    {
        var card = await cardRepository.GetCardAsync(id, cancellationToken);
        if (card == null)
            return Results.NotFound();

        var category = await categoryRepository.GetCategoryAsync(card.CategoryId, cancellationToken);
        if (category == null)
            return Results.NotFound();

        var categoryAttributeMappings = await categoryRepository.GetCategoryAttributeMappingsAsync(card.CategoryId, cancellationToken);
        var attributes = await attributeRepository.GetAttributesByIdsAsync(categoryAttributeMappings.Select(x => x.AttributeId).ToList(), cancellationToken);

        var response = new CardDetailDto
        {
            Category = category.ToDto(apiContext.CurrentCulture),
            Id = id,
            CreatedAt = card.CreatedAt,
            Attributes = new List<CardDetailDto.CardDetailAttributeDto>()
        };

        foreach (var cardAttribute in card.Attributes)
        {
            var attribute = attributes.FirstOrDefault(q => q.Id == cardAttribute.AttributeId);
            if (attribute == null)
                continue;

            var categoryAttributeMapping = categoryAttributeMappings.FirstOrDefault(q => q.AttributeId == cardAttribute.AttributeId && q.CategoryId == card.CategoryId);
            if (categoryAttributeMapping == null)
                continue;

            response.Attributes.Add(new CardDetailDto.CardDetailAttributeDto
            {
                Items = attribute.Items.Select(q => new AttributeDto.AttributeItemModel
                {
                    Id = q.Id,
                    Icon = q.Icon,
                    ItemValue = q.ItemValue,
                    DetailPosition = categoryAttributeMapping.DetailPosition,
                    ListingPosition = categoryAttributeMapping.ListingPosition
                }).ToList(),
                Label = attribute.Translations.FirstOrDefault(q => q.Culture == apiContext.CurrentCulture),
                Values = cardAttribute.Values
            });
        }

        await searchService.IndexCardAsync(card.Id, cancellationToken);
        return Results.Ok(response);
    }


    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/card/{id}", Handler).Produces<CardDetailDto>().WithTags("Card");
    }
}