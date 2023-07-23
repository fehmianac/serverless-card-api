using Domain.Entities;
using Domain.Entities.Shared;

namespace Api.Endpoints.V1.Model.Attribute;

public class AttributeSaveRequestModel
{
    public string SystemName { get; set; } = default!;
    public List<TranslationModel> Translations { get; set; } = new();
    public AttributeType Type { get; set; }
    public List<AttributeItemModel> Items { get; set; } = new();

    public class AttributeItemModel
    {
        public string? Id { get; set; } = default!;
        public AttributeValueModel ItemValue { get; set; } = default!;
    }
}