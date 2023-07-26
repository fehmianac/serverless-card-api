using Domain.Dto;
using Domain.Entities;

namespace Domain.Mappers;

public static class AttributeMapper
{
    public static AttributeDto ToDto(this AttributeEntity entity)
    {
        return new AttributeDto
        {
            Id = entity.Id,
            Items = entity.Items.Select(q => new AttributeDto.AttributeItemModel
            {
                Id = q.Id,
                ItemValue = q.ItemValue,
                Icon = q.Icon
            }).ToList(),
            Translations = entity.Translations,
            Type = entity.Type,
            SystemName = entity.SystemName
        };
    }
}