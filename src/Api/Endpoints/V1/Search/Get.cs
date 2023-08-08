using Api.Infrastructure.Contract;
using Domain.Models.Search;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Search;

public class Get : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromQuery] string categoryId,
        [FromBody] SearchRequestModel request,
        [FromServices] ISearchService searchService, CancellationToken cancellationToken)
    {
        request.CategoryId = categoryId;
        var response = await searchService.Search(request, cancellationToken);
        return Results.Ok(response);
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/api/v1/search", Handler).Produces(StatusCodes.Status200OK).WithTags("Search");
    }
}