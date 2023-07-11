using System.Text.Json;
using Api.Infrastructure.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints.V1.Card;

public class Post : IEndpoint
{
    private static async Task<IResult> Handler([FromBody] CardPostRequest request, CancellationToken cancellationToken)
    {
        return Results.Ok();
    }

    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/api/v1/card", Handler);
    }
}

public class CardPostRequest
{
    public string CategoryId { get; set; } = default!;
    public List<AttributeItem> AttributeItems { get; set; }

    public class AttributeItem
    {
        public string AttributeId { get; set; }
        public JsonElement Value { get; set; }
    }
    //public Dictionary<string, JsonElement> Attributes { get; set; } = new Dictionary<string, JsonElement>();
}