using System.Text.Json.Serialization;
using Domain.Entities.Base;
using Domain.Entities.Shared;

namespace Domain.Entities;

public class CardEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => "cards";
    [JsonPropertyName("sk")] public string Sk => Id;
    [JsonPropertyName("id")] public string Id { get; set; } = default!;
    [JsonPropertyName("userId")] public string UserId { get; set; } = default!;
    [JsonPropertyName("categoryId")] public string CategoryId { get; set; } = default!;
    [JsonPropertyName("attributes")] public List<CardAttributeMappingModel> Attributes { get; set; } = new();
    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updatedAt")] public DateTime UpdatedAt { get; set; }

    public class CardAttributeMappingModel
    {
        [JsonPropertyName("attributeId")] public string AttributeId { get; set; } = default!;
        [JsonPropertyName("attributeItemId")] public string? AttributeItemId { get; set; }
        [JsonPropertyName("values")] public List<AttributeValueModel> Values { get; set; } = new();
    }
}