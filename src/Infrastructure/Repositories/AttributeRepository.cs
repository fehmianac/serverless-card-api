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
}