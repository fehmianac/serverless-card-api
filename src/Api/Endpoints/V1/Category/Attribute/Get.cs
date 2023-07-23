using Api.Infrastructure.Contract;
using Domain.Mappers;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Category.Attribute;

public class Get : IEndpoint
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

        return Results.Ok(entity.ToDto());
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/category/{categoryId}/attribute/{attributeId}", Handler)
            .Produces(StatusCodes.Status200OK)
            .WithTags("Category");
    }
}