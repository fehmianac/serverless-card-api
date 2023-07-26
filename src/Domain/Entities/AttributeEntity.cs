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
    [JsonPropertyName("items")] public List<AttributeItemModel> Items { get; set; } = new();

    public class AttributeItemModel
    {
        [JsonPropertyName("id")] public string Id { get; set; } = default!;
        
        [JsonPropertyName("icon")] public string? Icon { get; set; }
        [JsonPropertyName("itemValue")] public AttributeValueModel ItemValue { get; set; } = default!;
    }
}

public enum AttributeType : short
{
    String = 0,
    StringMultiLine = 1,
    Numeric = 2,
    NumericRange = 3,
    Boolean = 4,
    ListSelect = 5,
    Price = 6,
    PriceRange = 7,
    Date = 8,
    DateRange = 9,
    Location = 10,
}