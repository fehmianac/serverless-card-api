using Api.Infrastructure.Contract;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Category.Attribute;

public class Delete : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string categoryId,
        [FromRoute] string attributeId,
        [FromServices] ICategoryRepository categoryRepository,
        CancellationToken cancellationToken)
    {
        var entity = await categoryRepository.GetCategoryAttributeMappingAsync(categoryId, attributeId, cancellationToken);
        if (entity == null)
            return Results.NotFound();

        await categoryRepository.DeleteCategoryAttributeAsync(categoryId, attributeId, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapDelete("/api/v1/category/{categoryId}/attribute/{attributeId}", Handler)
            .Produces(StatusCodes.Status200OK)
            .WithTags("Category");
    }
}