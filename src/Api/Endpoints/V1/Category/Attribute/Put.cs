using Api.Endpoints.V1.Model.Category.Attribute;
using Api.Infrastructure.Contract;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Category.Attribute;

public class Put : IEndpoint
{
    private static async Task<IResult> Handler([FromRoute] string categoryId,
        [FromRoute] string attributeId,
        [FromBody] CategoryAttributeSaveRequestModel request,
        [FromServices] ICategoryRepository categoryRepository,
        CancellationToken cancellationToken)
    {
        var isExist = (await categoryRepository.GetCategoryAttributeMappingAsync(categoryId, attributeId, cancellationToken)) != null;
        var entity = new CategoryAttributeMappingEntity
        {
            Page = request.Page,
            Rank = request.Rank,
            IsRequired = request.IsRequired,
            AttributeId = attributeId,
            CategoryId = categoryId,
            DefaultValues = request.DefaultValues,
            IsFilterable = request.IsFilterable,
            IsSingleSelection = request.IsSingleSelection,
            DetailPosition = request.DetailPosition,
            ListingPosition = request.ListingPosition,
            ShowOnListing = request.ShowOnListing
        };
        await categoryRepository.SaveCategoryAttributeAsync(entity, cancellationToken);
        return isExist ? Results.Ok() : Results.Created("/api/v1/category/{categoryId}/attribute/{attributeId}", new {entity.CategoryId, entity.AttributeId});
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPut("/api/v1/category/{categoryId}/attribute/{attributeId}", Handler)
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status201Created)
            .WithTags("Category");
    }
}