using System.Text.Json.Serialization;

namespace Domain.Entities.Shared;

public class AttributeValueModel
{
    [JsonPropertyName("stringValue")] public string? StringValue { get; set; }
    [JsonPropertyName("numericValue")] public decimal? NumericValue { get; set; }
    [JsonPropertyName("boolValue")] public bool? BoolValue { get; set; }
    [JsonPropertyName("dateTimeValue")] public DateTime? DateTimeValue { get; set; }
    
    [JsonPropertyName("priceValue")] public AttributeValuePriceModel? PriceValue { get; set; }
    [JsonPropertyName("locationValue")] public AttributeValueLocationModel? LocationValue { get; set; }

    public class AttributeValueLocationModel
    {
        [JsonPropertyName("lat")] public double Lat { get; set; }
        [JsonPropertyName("lng")] public double Lng { get; set; }
    }

    public class AttributeValuePriceModel
    {
        [JsonPropertyName("price")]
        public double? Price { get; set; }
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }
    }
}