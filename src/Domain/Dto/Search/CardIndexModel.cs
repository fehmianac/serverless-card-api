using Nest;

namespace Domain.Dto.Search;

public class CardIndexModel
{
    [Keyword(Name = "id")] public string Id { get; set; } = default!;

    [Keyword(Name = "categoryId")] public string CategoryId { get; set; } = default!;

    [Keyword(Name = "categoryName")] public string CategoryName { get; set; } = default!;

    [Nested(Name = "attributes")] public List<CardIndexAttributeModel> Attributes { get; set; } = new();

    public class CardIndexAttributeModel
    {
        [Nested(Name = "stringValues")] public List<StringAttributeItemModel> StringValues { get; set; } = new();
        [Nested(Name = "numericValues")] public List<NumericAttributeItemModel> NumericValues { get; set; } = new();
        [Nested(Name = "numericRangeValues")] public List<NumericRangeAttributeItemModel> NumericRangeValues { get; set; } = new();
        [Nested(Name = "booleanValues")] public List<BooleanAttributeItemModel> BooleanValues { get; set; } = new();
        [Nested(Name = "dateValues")] public List<DateAttributeItemModel> DateValues { get; set; } = new();
        [Nested(Name = "dateRangeValues")] public List<DateRangeAttributeItemModel> DateRangeValues { get; set; } = new();
        [Nested(Name = "locationValues")] public List<LocationAttributeItemModel> LocationValues { get; set; } = new();
        [Nested(Name = "selectedItems")] public List<SelectedItemsAttributeItemModel> SelectedItems { get; set; } = new();
    }

    public class CardIndexAttributeBaseItemModel
    {
        [Keyword(Name = "attributeId")] public string AttributeId { get; set; } = default!;
    }

    public class StringAttributeItemModel : CardIndexAttributeBaseItemModel
    {
        [Keyword(Name = "value")] public string Value { get; set; } = default!;
    }

    public class NumericAttributeItemModel : CardIndexAttributeBaseItemModel
    {
        [Number(Name = "value")] public decimal Value { get; set; }
    }

    public class NumericRangeAttributeItemModel : CardIndexAttributeBaseItemModel
    {
        [Number(Name = "min")] public decimal Min { get; set; }
        [Number(Name = "max")] public decimal Max { get; set; }
    }

    public class BooleanAttributeItemModel : CardIndexAttributeBaseItemModel
    {
        [Boolean(Name = "value")] public bool Value { get; set; }
    }

    public class DateAttributeItemModel : CardIndexAttributeBaseItemModel
    {
        [Date(Name = "value")] public DateTime Value { get; set; }
    }

    public class DateRangeAttributeItemModel : CardIndexAttributeBaseItemModel
    {
        [Date(Name = "min")] public DateTime Min { get; set; }
        [Date(Name = "max")] public DateTime Max { get; set; }
    }

    public class LocationAttributeItemModel : CardIndexAttributeBaseItemModel
    {
        [GeoPoint(Name = "value")] public GeoLocation Value { get; set; } = default!;
    }

    public class SelectedItemsAttributeItemModel : CardIndexAttributeBaseItemModel
    {
        [Keyword(Name = "value")] public string Value { get; set; } = default!;
    }
}