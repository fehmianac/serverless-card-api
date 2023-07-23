using Api.Infrastructure.Context;
using Api.Infrastructure.Contract;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Category;

public class Delete : IEndpoint
{
    private static async Task<IResult> Handler([FromRoute] string id,
        [FromServices] IApiContext apiContext,
        [FromServices] ICategoryRepository categoryRepository,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetCategoryAsync(id, cancellationToken);
        if (category == null)
            return Results.NotFound();

        await categoryRepository.DeleteCategoryAsync(category, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapDelete("/api/v1/category/{id}", Handler)
            .Produces(StatusCodes.Status200OK)
            .WithTags("Category");
    }
}