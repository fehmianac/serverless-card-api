using Api.Infrastructure.Contract;

namespace Api.Endpoints.V1.Attribute;

public class GetPaged : IEndpoint
{
    private static async Task<IResult> Handler()
    {
        return Results.Ok();
    }
    public void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/api/v1/attributes", Handler);
    }
}