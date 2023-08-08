using Domain.Dto.Search;

namespace Domain.Models.Search;

public class SearchResponseModel
{
    public List<CardIndexModel> Cards { get; set; } = new();
    public long Total { get; set; }
    public FilterModel Filters { get; set; } = default!;

    public class FilterModel
    {
    }
}