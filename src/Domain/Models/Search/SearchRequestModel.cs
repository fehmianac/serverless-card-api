namespace Domain.Models.Search;

public class SearchRequestModel
{
    public string CategoryId { get; set; } = default!;
    public int? From { get; set; }
    public int? Size { get; set; }
}