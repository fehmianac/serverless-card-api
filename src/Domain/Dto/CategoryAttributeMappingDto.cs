using Domain.Entities.Shared;

namespace Domain.Dto;

public class CategoryAttributeMappingDto
{
    public string AttributeId { get; set; } = default!;
    public string CategoryId { get; set; } = default!;
    public int Page { get; set; }
    public List<AttributeValueModel> DefaultValues { get; set; } = new();
    public bool IsFilterable { get; set; }

    public decimal Boosting { get; set; }
    public bool IsSingleSelection { get; set; }
    public bool IsRequired { get; set; }
    public bool ShowOnListing { get; set; }
    public string? ListingPosition { get; set; }
    public string? DetailPosition { get; set; }
    public int Rank { get; set; }
}