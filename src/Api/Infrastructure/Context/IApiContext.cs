namespace Api.Infrastructure.Context;

public interface IApiContext
{
    string CurrentUserId { get; set; }
    string CurrentCulture { get; set; }
}

public class ApiContext : IApiContext
{
    public string CurrentUserId { get; set; } = "123";
    public string CurrentCulture { get; set; } = "tr-TR";
}