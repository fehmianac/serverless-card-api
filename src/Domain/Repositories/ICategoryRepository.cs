using Domain.Entities;

namespace Domain.Repositories;

public interface ICategoryRepository
{
    Task<CategoryEntity?> GetCategoryAsync(string categoryId, CancellationToken cancellationToken);

    Task<List<CategoryAttributeMappingEntity>> GetCategoryAttributeMappingsAsync(string categoryId, CancellationToken cancellationToken);
    Task DeleteCategoryAsync(CategoryEntity category, CancellationToken cancellationToken);
    Task SaveCategoryAsync(CategoryEntity categoryEntity, CancellationToken cancellationToken);
    Task<(List<CategoryEntity> result, string token)> GetCategoriesAsync(string? nextToken, int limit, CancellationToken cancellationToken);
    Task SaveCategoryAttributeAsync(CategoryAttributeMappingEntity entity, CancellationToken cancellationToken);
    Task<CategoryAttributeMappingEntity?> GetCategoryAttributeMappingAsync(string categoryId, string attributeId, CancellationToken cancellationToken);
    Task DeleteCategoryAttributeAsync(string categoryId, string attributeId, CancellationToken cancellationToken);
    Task<(List<CategoryAttributeMappingEntity> entities, string token)> GetCategoryAttributeMappingsPagedAsync(string categoryId, string? nextToken, int limit, CancellationToken cancellationToken);
}