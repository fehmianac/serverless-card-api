using Domain.Dto.Search;
using Domain.Services;
using Nest;

namespace Infrastructure.Services;

public class SearchService : ISearchService
{
    private const string IndexName = "card-index";
    private readonly IElasticClient _elasticClient;

    public SearchService(IElasticClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<bool> CreateIndexAsync(CancellationToken cancellationToken = default)
    {
        var isExist = await _elasticClient.Indices.ExistsAsync(new IndexExistsRequest(IndexName), cancellationToken);
        if (isExist.Exists)
        {
            return true;
        }

        var indexResponse = await _elasticClient.Indices.CreateAsync(IndexName, descriptor => descriptor.Map<CardIndexModel>(q => q.AutoMap()), cancellationToken);
        return indexResponse.IsValid;
    }

    public Task<bool> DeleteIndexAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IndexCardAsync(string cardId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}