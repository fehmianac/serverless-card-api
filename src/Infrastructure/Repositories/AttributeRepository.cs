using Amazon.DynamoDBv2;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class AttributeRepository : DynamoRepository, IAttributeRepository
{
    public AttributeRepository(IAmazonDynamoDB dynamoDb) : base(dynamoDb)
    {
    }

    protected override string GetTableName() => "cards";

    public async Task<bool> SaveAttributeAsync(AttributeEntity attribute, CancellationToken cancellationToken)
    {
        return await SaveAsync(attribute, cancellationToken);
    }

    public async Task<List<AttributeEntity>> GetAttributesByIdsAsync(List<string> ids, CancellationToken cancellationToken)
    {
        return await BatchGetAsync(ids.Select(q => new AttributeEntity
        {
            Id = q
        }).ToList(), cancellationToken);
    }

    public async Task<AttributeEntity?> GetAttributeAsync(string id, CancellationToken cancellationToken)
    {
        return await GetAsync<AttributeEntity>("attributes", id, cancellationToken);
    }

    public async Task DeleteAttributeAsync(string id, CancellationToken cancellationToken)
    {
        await DeleteAsync("attributes", id, cancellationToken);
    }

    public async Task<(List<AttributeEntity> result, string token)> GetAttributesAsync(string? nextToken, int limit, CancellationToken cancellationToken)
    {
        var (result, token, _) = await GetPagedAsync<AttributeEntity>("attributes", nextToken, limit, cancellationToken);
        return (result, token);
    }
}