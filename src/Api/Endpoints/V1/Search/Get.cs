using Api.Infrastructure.Contract;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Search;

public class Get : IEndpoint
{
    private static async Task<IResult> Handler([FromServices] ISearchService searchService, CancellationToken cancellationToken)
    {
        await searchService.CreateIndexAsync(cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/search", Handler).Produces(StatusCodes.Status200OK).WithTags("Search");
    }
}