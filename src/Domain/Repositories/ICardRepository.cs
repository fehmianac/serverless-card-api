using Domain.Entities;

namespace Domain.Repositories;

public interface ICardRepository
{
    Task<bool> CardSaveAsync(CardEntity cardEntity, CancellationToken cancellationToken);
    Task<CardEntity?> GetCardAsync(string id, CancellationToken cancellationToken);
    Task CardDeleteAsync(string id, string userId, CancellationToken cancellationToken);
}