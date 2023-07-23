using Api.Infrastructure.Contract;
using Domain.Dto;
using Domain.Mappers;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Attribute;

public class Get : IEndpoint
{
    private static async Task<IResult> Handler([FromRoute] string id, [FromServices] IAttributeRepository attributeRepository, CancellationToken cancellationToken)
    {
        var entity = await attributeRepository.GetAttributeAsync(id, cancellationToken);
        if (entity == null)
            return Results.NotFound();

        return Results.Ok(entity.ToDto());
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/attribute/{id}", Handler)
            .Produces<AttributeDto>()
            .WithTags("Attribute");
    }
}