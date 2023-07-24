using Api.Infrastructure.Contract;
using Domain.Entities.Shared;

namespace Api.Endpoints.V1.Card;

public class GetPaged : IEndpoint
{
    private static async Task<IResult> Handler()
    {
        var list = new List<object>();
        list.Add(new StringValue
        {
            Value = new TranslationModel
            {
                Culture = "tr"
            }
        });
        list.Add(new IntValue
        {
            Value = 1
        });
        list.Add(new BoolValue
        {
            Value = true
        });
        return Results.Ok(list);
    }

    public RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        return endpoints.MapGet("/api/v1/card", Handler);
    }
}

public interface IAttributeValue
{
}

public class StringValue : IAttributeValue
{
    public TranslationModel Value { get; set; }
}

public class IntValue : IAttributeValue
{
    public int Value { get; set; }
}

public class BoolValue : IAttributeValue
{
    public bool Value { get; set; }
}