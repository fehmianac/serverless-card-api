using System.Text.Json.Serialization;
using Domain.Entities.Base;
using Domain.Entities.Shared;

namespace Domain.Entities;

public class CategoryAttributeMappingEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => $"categoryAttributeMappings#{CategoryId}";
    [JsonPropertyName("sk")] public string Sk => $"{AttributeId}";

    [JsonPropertyName("attributeId")] public string AttributeId { get; set; } = default!;
    [JsonPropertyName("categoryId")] public string CategoryId { get; set; } = default!;
    [JsonPropertyName("defaultValues")] public List<AttributeValueModel> DefaultValues { get; set; } = new();
    [JsonPropertyName("isFilterable")] public bool IsFilterable { get; set; }
    [JsonPropertyName("isSingleSelection")] public bool IsSingleSelection { get; set; }
    [JsonPropertyName("isRequired")] public bool IsRequired { get; set; }
    [JsonPropertyName("rank")] public int Rank { get; set; }
}