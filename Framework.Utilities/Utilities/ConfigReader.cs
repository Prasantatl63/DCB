using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;


namespace DCB.Framework.Utilities
{
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot Configuration;

        static ConfigReader()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string Browser => Configuration["Browser"] ?? "chrome";

        public static bool Headless => 
        bool.TryParse(Configuration["Headless"], out var result) && result;

        public static string ? BaseUrl => Configuration["BaseUrl"];
    }
}