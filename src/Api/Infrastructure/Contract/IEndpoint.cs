namespace Api.Infrastructure.Contract;

public interface IEndpoint
{
    RouteHandlerBuilder MapEndpoint(IEndpointRouteBuilder endpoints);
}