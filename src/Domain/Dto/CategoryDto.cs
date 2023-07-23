namespace Domain.Dto;

public class CategoryDto
{
    public string Id { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public string Label { get; set; } = default!;
    public string? Placeholder { get; set; }
    public string? Description { get; set; }
    public string? Hint { get; set; }
}