using System.Text.Json.Serialization;
using Domain.Entities.Base;
using Domain.Entities.Shared;

namespace Domain.Entities;

public class CategoryEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => "categories";

    [JsonPropertyName("sk")] public string Sk => $"{Id}";
    [JsonPropertyName("id")] public string Id { get; set; } = default!;
    [JsonPropertyName("name")] public string Name { get; set; } = default!;
    [JsonPropertyName("translations")] public List<TranslationModel> Translations { get; set; } = new();
    [JsonPropertyName("imageUrl")] public string? ImageUrl { get; set; }
    [JsonPropertyName("createdAt")] public DateTime CreatedAt { get; set; }
    [JsonPropertyName("updatedAt")] public DateTime UpdatedAt { get; set; }
}