using Api.Endpoints.V1.Model.Attribute;
using Api.Infrastructure.Contract;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Attribute;

public class Put : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromRoute] string id,
        [FromBody] AttributeSaveRequestModel request,
        [FromServices] IAttributeRepository attributeRepository,
        CancellationToken cancellationToken)
    {
        var attributeEntity = await attributeRepository.GetAttributeAsync(id, cancellationToken);
        if (attributeEntity == null)
            return Results.NotFound();


        attributeEntity.Items = request.Items.Select(q => new AttributeEntity.AttributeItemModel
        {
            Id = q.Id ?? Guid.NewGuid().ToString("N"),
            ItemValue = q.ItemValue
        }).ToList();
        attributeEntity.Translations = request.Translations;
        attributeEntity.Type = request.Type;
        attributeEntity.SystemName = request.SystemName;

        await attributeRepository.SaveAttributeAsync(attributeEntity, cancellationToken);

        return Results.Ok();
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPut("/api/v1/attribute/{id}", Handler)
            .Produces(StatusCodes.Status200OK)
            .WithTags("Attribute");
    }
}