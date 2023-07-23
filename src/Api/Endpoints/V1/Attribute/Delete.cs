using Api.Infrastructure.Contract;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Attribute;

public class Delete : IEndpoint
{
    private async Task<IResult> Handler(
        [FromRoute] string id,
        [FromServices] IAttributeRepository attributeRepository,
        CancellationToken cancellationToken)
    {
        var attributeEntity = await attributeRepository.GetAttributeAsync(id, cancellationToken);
        if (attributeEntity == null)
            return Results.NotFound();

        await attributeRepository.DeleteAttributeAsync(id, cancellationToken);
        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapDelete("/api/v1/attribute/{id}", Handler).Produces(StatusCodes.Status200OK).WithTags("Attribute");
    }
}