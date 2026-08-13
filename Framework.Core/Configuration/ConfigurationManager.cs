using Microsoft.Extensions.Configuration;

namespace DCB.Framework.Configuration;

public static class ConfigurationManager
{
    private static readonly Lazy<IConfigurationRoot> _configuration =
        new(BuildConfiguration);

    public static IConfigurationRoot Configuration =>
        _configuration.Value;

    public static FrameworkSettings Settings =>
        Configuration
            .Get<FrameworkSettings>()
            ?? new FrameworkSettings();

    private static IConfigurationRoot BuildConfiguration()
    {
        var environment =
            Environment.GetEnvironmentVariable("ENVIRONMENT")
            ?? "QA";

        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(
                "Configuration/appsettings.json",
                optional: false,
                reloadOnChange: false)
            .AddJsonFile(
                $"appsettings.{environment}.json",
                optional: true,
                reloadOnChange: false)
            .AddEnvironmentVariables()
            .Build();
    }
}