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

        public static string Browser => Configuration["TestSettings:Browser"] ?? "chromium";

        public static bool Headless => 
        bool.TryParse(Configuration["TestSettings:Headless"], out var result) && result;


        public static string? BaseUrl => Configuration["TestSettings:BaseUrl"];

        public static string? UserName => Configuration["TestSettings:UserName"];
        public static string? Password => Configuration["TestSettings:Password"];

        public static int DefaultTimeout =>
        int.TryParse(Configuration["TestSettings:DefaultTimeout"], out var timeout)
            ? timeout
            : 30000;
    }
}