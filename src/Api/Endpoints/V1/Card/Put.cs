using Api.Endpoints.V1.Model.Card;
using Api.Infrastructure.Context;
using Api.Infrastructure.Contract;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Card;

public class Put : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string id,
        [FromBody] CardSaveRequestModel request,
        [FromServices] IApiContext apiContext,
        [FromServices] ICardRepository cardRepository,
        CancellationToken cancellationToken)
    {
        //TODO validate,

        var card = await cardRepository.GetCardAsync(id, cancellationToken);
        if (card == null)
        {
            return Results.NotFound();
        }

        if (apiContext.CurrentUserId != card.UserId)
        {
            return Results.Forbid();
        }

        card.UpdatedAt = DateTime.UtcNow;
        card.Attributes.Clear();
        foreach (var attributeItem in request.Items)
        {
            card.Attributes.Add(new CardEntity.CardAttributeMappingModel
            {
                AttributeId = attributeItem.AttributeId,
                Values = attributeItem.Values
            });
        }

        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPut("/api/v1/card/{id}", Handler).Produces(StatusCodes.Status200OK).WithTags("Card");
    }
}