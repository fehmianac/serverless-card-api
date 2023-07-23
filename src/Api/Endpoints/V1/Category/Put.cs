using Api.Endpoints.V1.Model.Category;
using Api.Infrastructure.Contract;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Category;

public class Put : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string id,
        [FromBody] CategorySaveRequestModel request,
        [FromServices] ICategoryRepository categoryRepository,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetCategoryAsync(id, cancellationToken);
        if (category == null)
            return Results.NotFound();

        category.Translations = request.Translations;
        category.Name = request.Name;
        category.ImageUrl = request.ImageUrl;
        category.UpdatedAt = DateTime.UtcNow;

        await categoryRepository.SaveCategoryAsync(category, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPut("/api/v1/category/{id}", Handler)
            .Produces(StatusCodes.Status200OK)
            .WithTags("Category");
    }
}