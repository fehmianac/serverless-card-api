using System.Reflection;
using Api.Infrastructure.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

public static class StartupExtensions
{
    public static IEndpointRouteBuilder MapEndpointsCore(this IEndpointRouteBuilder endpoints, IEnumerable<Assembly> assemblies)
    {
        var endpointTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t));

        foreach (var endpointType in endpointTypes)
        {
            if (endpointType.IsInterface)
            {
                continue;
            }

            var endpoint = Activator.CreateInstance(endpointType);
            if (endpoint is IEndpoint agadaEndpoint)
            {
                try
                {
                    var mappedEndpoint = agadaEndpoint.MapEndpoint(endpoints);
                    mappedEndpoint.Produces(StatusCodes.Status400BadRequest, typeof(ProblemDetails), "application/problem+json");
                    mappedEndpoint.Produces(StatusCodes.Status403Forbidden, typeof(ProblemDetails), "application/problem+json");
                    mappedEndpoint.Produces(StatusCodes.Status404NotFound);
                    mappedEndpoint.Produces(StatusCodes.Status500InternalServerError, typeof(ProblemDetails), "application/problem+json");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
        endpoints.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
        return endpoints;
    }

    public static string CustomSchemaId(this string input, string nameSpace)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var nameSpaceArray = nameSpace!.Split('.');
        var nameSpacePrefix = "";
        foreach (var nameSpaceItem in nameSpaceArray)
        {
            if (nameSpaceItem.StartsWith("V"))
            {
                break;
            }

            nameSpacePrefix += nameSpaceItem + ".";
        }

        var result = input.Replace("+", string.Empty)
            .Replace("[", string.Empty)
            .Replace("]", string.Empty)
            .Replace("`1", string.Empty)
            .Replace(nameSpacePrefix, string.Empty)
            .Replace(".", string.Empty);

        if (!result.Contains(','))
            return $"{result}";

        var indexOfComma = result.IndexOf(',');
        return $"{result[..indexOfComma]}";
    }
}