using Api.Infrastructure.Contract;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Card;

public class Get : IEndpoint
{
    private static async Task<IResult> Handler([FromRoute] string id, CancellationToken cancellationToken)
    {
        return Results.Ok();
    }


    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/card/{id}", Handler).Produces<CardDetailDto>().WithTags("Card");
    }
}