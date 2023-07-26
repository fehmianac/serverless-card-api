using Domain.Dto;
using Domain.Entities;

namespace Domain.Mappers;

public static class CategoryMapper
{
    public static CategoryDto ToDto(this CategoryEntity entity, string culture)
    {
        var translation = entity.Translations.FirstOrDefault(q => q.Culture == culture);

        if (translation == null)
            translation = entity.Translations.FirstOrDefault();


        return new CategoryDto
        {
            Id = entity.Id,
            Description = translation?.Description,
            Hint = translation?.Hint,
            Label = translation?.Label ?? "",
            Placeholder = translation.Placeholder,
            ImageUrl = entity.ImageUrl
        };
    }

    public static CategoryAttributeMappingDto ToDto(this CategoryAttributeMappingEntity entity)
    {
        return new CategoryAttributeMappingDto
        {
            Page = entity.Page,
            Rank = entity.Rank,
            AttributeId = entity.AttributeId,
            CategoryId = entity.CategoryId,
            DefaultValues = entity.DefaultValues,
            IsFilterable = entity.IsFilterable,
            IsRequired = entity.IsRequired,
            IsSingleSelection = entity.IsSingleSelection,
            DetailPosition = entity.DetailPosition,
            ListingPosition = entity.ListingPosition,
            ShowOnListing = entity.ShowOnListing
        };
    }
}