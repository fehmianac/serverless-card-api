using Api.Infrastructure.Contract;
using Domain.Entities.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Card;

public class GetPaged : IEndpoint
{
    private static async Task<IResult> Handler([FromRoute] string userId)
    {
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/user/{userId}/card/", Handler).WithTags("Card");
    }
}