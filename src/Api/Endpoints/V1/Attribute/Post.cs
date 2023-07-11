using Api.Infrastructure.Contract;
using Domain.Entities;
using Domain.Entities.Shared;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Attribute;

public class Post : IEndpoint
{
    private async Task<IResult> Handler(
        [FromBody] AttributeEntity request,
        [FromServices] IAttributeRepository attributeRepository,
        CancellationToken cancellationToken)
    {
        await attributeRepository.SaveAttributeAsync(new AttributeEntity
        {
            Id = Guid.NewGuid().ToString("N"),
            Type = AttributeType.String,
            DefaultValues = new List<AttributeValueModel>
            {
                new AttributeValueModel
                {
                    StringValue = "Default value"
                }
            },
            Translations = new List<TranslationModel>
            {
                new TranslationModel
                {
                    Culture = "tr-TR",
                    Label = "Kullanıcı Adı"
                }
            },
            SystemName = "UserName"
        }, cancellationToken);

        await attributeRepository.SaveAttributeAsync(new AttributeEntity
        {
            Id = Guid.NewGuid().ToString("N"),
            Type = AttributeType.Price,
            DefaultValues = new List<AttributeValueModel>
            {
                new AttributeValueModel
                {
                    PriceValue = new AttributeValueModel.AttributeValuePriceModel
                    {
                        Currency = "TRY",
                        Price = 100.99
                    }
                }
            },
            Translations = new List<TranslationModel>
            {
                new TranslationModel
                {
                    Culture = "tr-TR",
                    Label = "Kira"
                }
            },
            SystemName = "Rent"
        }, cancellationToken);
        
        await attributeRepository.SaveAttributeAsync(new AttributeEntity
        {
            Id = Guid.NewGuid().ToString("N"),
            Type = AttributeType.Location,
            DefaultValues = new List<AttributeValueModel>(),
            Translations = new List<TranslationModel>
            {
                new TranslationModel
                {
                    Culture = "tr-TR",
                    Label = "Konum"
                }
            },
            SystemName = "Location"
        }, cancellationToken);
        return Results.Ok();
    }

    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("v1/attribute", Handler);
    }
}