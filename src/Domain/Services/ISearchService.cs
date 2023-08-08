using Domain.Dto.Search;
using Domain.Models.Search;

namespace Domain.Services;

public interface ISearchService
{
    Task<bool> CreateIndexAsync(CancellationToken cancellationToken = default!);

    Task<bool> DeleteIndexAsync(CancellationToken cancellationToken = default!);

    Task<bool> IndexCardAsync(string cardId, CancellationToken cancellationToken = default!);

    Task<SearchResponseModel> Search(SearchRequestModel request, CancellationToken cancellationToken);
}