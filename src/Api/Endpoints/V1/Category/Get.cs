using Api.Infrastructure.Context;
using Api.Infrastructure.Contract;
using Domain.Dto;
using Domain.Mappers;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Category;

public class Get : IEndpoint
{
    private static async Task<IResult> Handler([FromRoute] string id,
        [FromServices] IApiContext apiContext,
        [FromServices] ICategoryRepository categoryRepository,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetCategoryAsync(id, cancellationToken);

        if (category == null)
            return Results.NotFound();

        return Results.Ok(category.ToDto(apiContext.CurrentCulture));
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/category/{id}", Handler)
            .Produces<CategoryDto>()
            .WithTags("Category");
    }
}