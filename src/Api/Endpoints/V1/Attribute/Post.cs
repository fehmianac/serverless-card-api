using Api.Endpoints.V1.Model.Attribute;
using Api.Infrastructure.Contract;
using Domain.Entities;
using Domain.Entities.Shared;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Attribute;

public class Post : IEndpoint
{
    private static async Task<IResult> Handler(
        [FromBody] AttributeSaveRequestModel request,
        [FromServices] IAttributeRepository attributeRepository,
        CancellationToken cancellationToken)
    {
        var attributeEntity = new AttributeEntity
        {
            Id = Guid.NewGuid().ToString("N"),
            Items = request.Items.Select(q => new AttributeEntity.AttributeItemModel
            {
                Id = q.Id ?? Guid.NewGuid().ToString("N"),
                Icon = q.Icon,
                ItemValue = q.ItemValue
            }).ToList(),
            Translations = request.Translations,
            Type = request.Type,
            SystemName = request.SystemName
        };

        await attributeRepository.SaveAttributeAsync(attributeEntity, cancellationToken);

        return Results.Created("/api/v1/attribute", attributeEntity.Id);
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapPost("/api/v1/attribute", Handler)
            .Produces(StatusCodes.Status201Created)
            .WithTags("Attribute");
    }
}