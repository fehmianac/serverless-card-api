using System.Text.Json.Serialization;
using Domain.Entities.Base;
using Domain.Entities.Shared;

namespace Domain.Entities;

public class AttributeEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => "attributes";

    [JsonPropertyName("sk")] public string Sk => $"{Id}";
    [JsonPropertyName("id")] public string Id { get; set; } = default!;
    [JsonPropertyName("systemName")] public string SystemName { get; set; } = default!;
    [JsonPropertyName("translations")] public List<TranslationModel> Translations { get; set; } = new();
    [JsonPropertyName("type")] public AttributeType Type { get; set; }
    [JsonPropertyName("defaultValues")] public List<AttributeValueModel> DefaultValues { get; set; } = new();
    [JsonPropertyName("items")] public List<AttributeItemModel> Items { get; set; } = new();

    public class AttributeItemModel
    {
        [JsonPropertyName("id")] public string Id { get; set; } = default!;
        [JsonPropertyName("translations")] public List<TranslationModel> Translations { get; set; } = new();
        [JsonPropertyName("itemValue")] public AttributeValueModel ItemValue { get; set; } = default!;
    }
}

public enum AttributeType
{
    String = 0,
    Number = 1,
    Boolean = 2,
    ListSelect = 3,
    Price = 4,
    Date = 5,
    Location = 6,
}