namespace Domain.Entities.Shared;

public class TranslationModel
{
    public string Culture { get; set; } = default!;
    public string Label { get; set; } = default!;
    public string? Placeholder { get; set; }
    public string? Description { get; set; }
    public string? Hint { get; set; }
}