using System.Text.Json.Serialization;

namespace Domain.Entities.Shared;

public class AttributeValueModel
{
    [JsonPropertyName("itemIds")] public List<string>? ItemIds { get; set; }
    [JsonPropertyName("stringValue")] public List<TranslationModel>? StringValue { get; set; }
    [JsonPropertyName("numericValue")] public decimal? NumericValue { get; set; }

    [JsonPropertyName("numericRangeValue")]
    public AttributeValueNumericRangeModel? NumericRangeValue { get; set; }

    [JsonPropertyName("boolValue")] public bool? BoolValue { get; set; }
    [JsonPropertyName("dateTimeValue")] public DateTime? DateTimeValue { get; set; }

    [JsonPropertyName("dateTimeRangeValue")]
    public AttributeValueDateTimeRangeModel? DateTimeRangeValue { get; set; }

    [JsonPropertyName("priceValue")] public AttributeValuePriceModel? PriceValue { get; set; }
    [JsonPropertyName("priceRangeValue")] public List<AttributeValuePriceRangeModel>? PriceRangeValue { get; set; }
    [JsonPropertyName("locationValue")] public AttributeValueLocationModel? LocationValue { get; set; }


    public class AttributeValueDateTimeRangeModel
    {
        [JsonPropertyName("min")] public DateTime Min { get; set; }
        [JsonPropertyName("max")] public DateTime Max { get; set; }
    }

    public class AttributeValueLocationModel
    {
        [JsonPropertyName("lat")] public double Lat { get; set; }
        [JsonPropertyName("lng")] public double Lng { get; set; }
    }

    public class AttributeValuePriceModel
    {
        [JsonPropertyName("price")] public double? Price { get; set; }
        [JsonPropertyName("currency")] public string? Currency { get; set; }
    }

    public class AttributeValuePriceRangeModel
    {
        [JsonPropertyName("min")] public double? Min { get; set; }
        [JsonPropertyName("max")] public double? Max { get; set; }
        [JsonPropertyName("currency")] public string? Currency { get; set; }
    }

    public class AttributeValueNumericRangeModel
    {
        [JsonPropertyName("min")] public double? Min { get; set; }
        [JsonPropertyName("max")] public double? Max { get; set; }
    }
}