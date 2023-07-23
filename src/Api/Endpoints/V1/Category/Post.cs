using Api.Endpoints.V1.Model.Category;
using Api.Infrastructure.Contract;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Category;

public class Post : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromBody] CategorySaveRequestModel request,
        [FromServices] ICategoryRepository categoryRepository,
        CancellationToken cancellationToken)
    {
        var categoryEntity = new CategoryEntity
        {
            Id = Guid.NewGuid().ToString("N"),
            Name = request.Name,
            Translations = request.Translations,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ImageUrl = request.ImageUrl
        };
        await categoryRepository.SaveCategoryAsync(categoryEntity, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/api/v1/category", Handler)
            .Produces(StatusCodes.Status201Created)
            .WithTags("Category");
    }
}