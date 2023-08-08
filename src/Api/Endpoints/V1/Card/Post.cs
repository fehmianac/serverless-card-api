using Api.Endpoints.V1.Model.Card;
using Api.Infrastructure.Context;
using Api.Infrastructure.Contract;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Card;

public class Post : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromBody] CardSaveRequestModel request,
        [FromServices] IApiContext apiContext,
        [FromServices] ICardRepository cardRepository,
        [FromServices] ISearchService searchService,
        CancellationToken cancellationToken)
    {
        //TODO validate,

        var card = new CardEntity
        {
            CategoryId = request.CategoryId,
            Id = Guid.NewGuid().ToString("N"),
            UserId = apiContext.CurrentUserId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        foreach (var attributeItem in request.Items)
        {
            card.Attributes.Add(new CardEntity.CardAttributeMappingModel
            {
                AttributeId = attributeItem.AttributeId,
                Values = attributeItem.Values
            });
        }

        await cardRepository.CardSaveAsync(card, cancellationToken);
        await searchService.IndexCardAsync(card.Id, cancellationToken);
        return Results.Created($"/api/v1/card/{card.Id}", card.Id);
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/api/v1/card", Handler).Produces(StatusCodes.Status201Created).WithTags("Card");
    }
}