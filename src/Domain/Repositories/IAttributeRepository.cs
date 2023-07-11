using Domain.Entities;

namespace Domain.Repositories;

public interface IAttributeRepository
{
    Task<bool> SaveAttributeAsync(AttributeEntity attribute, CancellationToken cancellationToken);
}