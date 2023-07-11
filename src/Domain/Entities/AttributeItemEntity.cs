using Domain.Entities.Base;
using Domain.Entities.Shared;

namespace Domain.Entities;

public class AttributeItemEntity : IEntity
{
    public string Pk { get; }
    public string Sk { get; }
    
    public List<AttributeItemEntity> Translations { get; set; } = new();
    public AttributeValueModel ItemValue { get; set; } = default!;

    public class AttributeItemTranslationModel
    {
        public string Culture { get; set; } = default!;
        public string Name { get; set; } = default!;
    }
}