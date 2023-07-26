using Domain.Entities;
using Domain.Entities.Shared;

namespace Domain.Dto;

public class CardStructureDto
{
    public CategoryDto Category { get; set; } = default!;
    public List<CardStructureAttributeItemDto> Attributes { get; set; } = new();

    public class CardStructureAttributeItemDto
    {
        public string Id { get; set; } = default!;
        public string SystemName { get; set; } = default!;
        public string Label { get; set; } = default!;
        public string? Placeholder { get; set; }
        public string? Description { get; set; }
        public string? Hint { get; set; }
        public AttributeType Type { get; set; }
        public int Page { get; set; }
        public List<AttributeValueModel> DefaultValues { get; set; } = new();
        public List<AttributeItemModel> Items { get; set; } = new();
        public bool IsSingleSelection { get; set; }
        public bool IsRequired { get; set; }
        public int Rank { get; set; }
    }
    public class AttributeItemModel
    {
        public string Id { get; set; } = default!;
        public AttributeValueModel ItemValue { get; set; } = default!;
        public string? Icon { get; set; }
    }
}
