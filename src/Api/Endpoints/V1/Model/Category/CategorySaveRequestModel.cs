using Domain.Entities.Shared;

namespace Api.Endpoints.V1.Model.Category;

public class CategorySaveRequestModel
{
    public string Name { get; set; } = default!;
    public List<TranslationModel> Translations { get; set; } = new();
    public string? ImageUrl { get; set; }
}