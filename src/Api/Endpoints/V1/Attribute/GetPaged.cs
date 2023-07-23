using Api.Infrastructure.Context;
using Api.Infrastructure.Contract;
using Domain.Dto;
using Domain.Dto.Base;
using Domain.Mappers;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Attribute;

public class GetPaged : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromQuery] int limit,
        [FromQuery] string? nextToken,
        [FromServices] IApiContext apiContext,
        [FromServices] IAttributeRepository attributeRepository,
        CancellationToken cancellationToken)
    {
        var (result, token) = await attributeRepository.GetAttributesAsync(nextToken, limit, cancellationToken);
        return Results.Ok(new PagedResponse<AttributeDto>
        {
            Data = result.Select(q => q.ToDto()).ToList(),
            Limit = limit,
            NextToken = token,
            PreviousToken = nextToken
        });
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/attributes", Handler)
            .Produces<PagedResponse<AttributeDto>>()
            .WithTags("Attribute");
    }
}