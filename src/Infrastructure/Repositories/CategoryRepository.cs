using Amazon.DynamoDBv2;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class CategoryRepository : DynamoRepository, ICategoryRepository
{
    public CategoryRepository(IAmazonDynamoDB dynamoDb) : base(dynamoDb)
    {
    }

    protected override string GetTableName() => "cards";

    public async Task<CategoryEntity?> GetCategoryAsync(string categoryId, CancellationToken cancellationToken)
    {
        return await GetAsync<CategoryEntity>("categories", categoryId, cancellationToken);
    }

    public async Task<List<CategoryAttributeMappingEntity>> GetCategoryAttributeMappingsAsync(string categoryId, CancellationToken cancellationToken)
    {
        return await GetAllAsync<CategoryAttributeMappingEntity>($"categoryAttributeMappings#{categoryId}", cancellationToken);
    }

    public async Task DeleteCategoryAsync(CategoryEntity category, CancellationToken cancellationToken)
    {
        await DeleteAsync("categories", category.Id, cancellationToken);
    }

    public async Task SaveCategoryAsync(CategoryEntity categoryEntity, CancellationToken cancellationToken)
    {
        await SaveAsync(categoryEntity, cancellationToken);
    }

    public async Task<(List<CategoryEntity> result, string token)> GetCategoriesAsync(string? nextToken, int limit, CancellationToken cancellationToken)
    {
        var (result, token, _) = await GetPagedAsync<CategoryEntity>("categories", nextToken, limit, cancellationToken);
        return (result, token);
    }

    public async Task SaveCategoryAttributeAsync(CategoryAttributeMappingEntity entity, CancellationToken cancellationToken)
    {
        await SaveAsync(entity, cancellationToken);
    }

    public async Task<CategoryAttributeMappingEntity?> GetCategoryAttributeMappingAsync(string categoryId, string attributeId, CancellationToken cancellationToken)
    {
        return await GetAsync<CategoryAttributeMappingEntity>($"categoryAttributeMappings#{categoryId}", attributeId, cancellationToken);
    }

    public Task DeleteCategoryAttributeAsync(string categoryId, string attributeId, CancellationToken cancellationToken)
    {
        return DeleteAsync($"categoryAttributeMappings#{categoryId}", attributeId, cancellationToken);
    }

    public async Task<(List<CategoryAttributeMappingEntity> entities, string token)> GetCategoryAttributeMappingsPagedAsync(string categoryId, string? nextToken, int limit, CancellationToken cancellationToken)
    {
        var (result, token, _) = await GetPagedAsync<CategoryAttributeMappingEntity>($"categoryAttributeMappings#{categoryId}", nextToken, limit, cancellationToken);
        return (result, token);
    }
}