using Domain.Entities.Shared;

namespace Domain.Dto;

public class CardDetailDto
{
    public string Id { get; set; } = default!;
    public CategoryDto Category { get; set; } = default!;
    public DateTime CreatedAt { get; set; }

    public List<CardDetailAttributeDto> Attributes { get; set; } = new();

    public class CardDetailAttributeDto
    {
        public TranslationModel Label { get; set; } = default!;
        public List<AttributeValueModel> Values { get; set; } = new();
        public List<AttributeDto.AttributeItemModel> Items { get; set; } = default!;
    }
}