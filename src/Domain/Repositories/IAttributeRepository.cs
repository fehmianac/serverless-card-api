using Domain.Entities;

namespace Domain.Repositories;

public interface IAttributeRepository
{
    Task<bool> SaveAttributeAsync(AttributeEntity attribute, CancellationToken cancellationToken);
    Task<List<AttributeEntity>> GetAttributesByIdsAsync(List<string> ids, CancellationToken cancellationToken);
    Task<AttributeEntity?> GetAttributeAsync(string id, CancellationToken cancellationToken);
    Task DeleteAttributeAsync(string id, CancellationToken cancellationToken);
    Task<(List<AttributeEntity> result, string token)> GetAttributesAsync(string? nextToken, int limit, CancellationToken cancellationToken);
}