using System.Text.Json.Serialization;
using Domain.Entities.Base;

namespace Domain.Entities;

public class UserCardsMappingEntity : IEntity
{
    [JsonPropertyName("pk")] public string Pk => $"userCards#{UserId}";
    [JsonPropertyName("sk")] public string Sk => $"{CardId}";
    [JsonPropertyName("userId")] public string UserId { get; set; } = default!;
    [JsonPropertyName("cardId")] public string CardId { get; set; } = default!;
}