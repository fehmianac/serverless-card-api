using Amazon.DynamoDBv2;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class CardRepository : DynamoRepository, ICardRepository
{
    public CardRepository(IAmazonDynamoDB dynamoDb) : base(dynamoDb)
    {
    }

    protected override string GetTableName() => "cards";

    public async Task<bool> CardSaveAsync(CardEntity cardEntity, CancellationToken cancellationToken)
    {
        await SaveAsync(cardEntity, cancellationToken);
        await SaveAsync(new UserCardsMappingEntity
        {
            CardId = cardEntity.Id,
            UserId = cardEntity.UserId
        }, cancellationToken);
        return true;
    }

    public async Task<CardEntity?> GetCardAsync(string id, CancellationToken cancellationToken)
    {
        return await GetAsync<CardEntity>("cards", id, cancellationToken);
    }

    public async Task CardDeleteAsync(string id, string userId, CancellationToken cancellationToken)
    {
        await DeleteAsync("cards", id, cancellationToken);
        await DeleteAsync($"userCards#{userId}", id, cancellationToken);
    }
}