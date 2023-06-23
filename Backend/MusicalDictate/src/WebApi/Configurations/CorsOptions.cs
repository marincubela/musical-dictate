namespace WebApi.Configurations;

public class CorsOptions
{
    public const string Cors = "Cors";

    public List<string> AllowedHosts { get; set; } = new();
    public string AllowedOrigins { get; set; } = string.Empty;
}