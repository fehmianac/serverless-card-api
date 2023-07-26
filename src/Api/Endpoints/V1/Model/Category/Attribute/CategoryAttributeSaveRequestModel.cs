using System.Text.Json.Serialization;
using Domain.Entities.Shared;

namespace Api.Endpoints.V1.Model.Category.Attribute;

public class CategoryAttributeSaveRequestModel
{
    public List<AttributeValueModel> DefaultValues { get; set; } = new();
    public bool IsFilterable { get; set; }
    public bool IsSingleSelection { get; set; }
    public bool IsRequired { get; set; }
    public bool ShowOnListing { get; set; }
    public string? ListingPosition { get; set; }
    public string? DetailPosition { get; set; }
    public int Rank { get; set; }
    public int Page { get; set; }
}