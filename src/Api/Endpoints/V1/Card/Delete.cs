using Api.Infrastructure.Context;
using Api.Infrastructure.Contract;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Card;

public class Delete : IEndpoint
{
    private static async Task<IResult> Handler([FromRoute] string id,
        [FromServices] IApiContext apiContext,
        [FromServices] ICardRepository cardRepository,
        CancellationToken cancellationToken)
    {
        var card = await cardRepository.GetCardAsync(id, cancellationToken);
        if (card == null)
            return Results.NotFound();

        if (apiContext.CurrentUserId != card.UserId)
            return Results.Forbid();

        await cardRepository.CardDeleteAsync(id, apiContext.CurrentUserId, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapDelete("/api/v1/card/{id}", Handler).Produces(StatusCodes.Status204NoContent).WithTags("Card");
    }
}