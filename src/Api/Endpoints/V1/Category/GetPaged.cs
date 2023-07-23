using Api.Infrastructure.Context;
using Api.Infrastructure.Contract;
using Domain.Dto;
using Domain.Dto.Base;
using Domain.Mappers;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Category;

public class GetPaged : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromQuery] int limit,
        [FromQuery] string? nextToken,
        [FromServices] IApiContext apiContext,
        [FromServices] ICategoryRepository categoryRepository,
        CancellationToken cancellationToken)
    {
        var (result, token) = await categoryRepository.GetCategoriesAsync(nextToken, limit, cancellationToken);
        return Results.Ok(new PagedResponse<CategoryDto>()
        {
            Limit = limit,
            Data = result.Select(q => q.ToDto(apiContext.CurrentCulture)).ToList(),
            NextToken = token,
            PreviousToken = nextToken
        });
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/category", Handler)
            .Produces<PagedResponse<CategoryDto>>()
            .WithTags("Category");
    }
}