using Api.Infrastructure.Contract;
using Domain.Dto;
using Domain.Dto.Base;
using Domain.Mappers;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Category.Attribute;

public class GetPaged : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string categoryId,
        [FromQuery] string? nextToken,
        [FromQuery] int? limit,
        [FromServices] ICategoryRepository categoryRepository,
        CancellationToken cancellationToken)
    {
        limit ??= 10;
        var (entities, token) = await categoryRepository.GetCategoryAttributeMappingsPagedAsync(categoryId, nextToken, limit.Value, cancellationToken);
        return Results.Ok(new PagedResponse<CategoryAttributeMappingDto>
        {
            Data = entities.Select(q => q.ToDto()).ToList(),
            Limit = limit.Value,
            NextToken = token,
            PreviousToken = nextToken
        });
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/category/{categoryId}/attribute", Handler)
            .Produces<PagedResponse<CategoryAttributeMappingDto>>()
            .WithTags("Category");
    }
}