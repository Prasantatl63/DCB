using Microsoft.Extensions.Configuration;

namespace DCB.Framework.Utilities
{
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot Configuration;

        //static ConfigReader()
        //{
        //    Configuration = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json")
        //        .Build();
        //}


        static ConfigReader()
        {
            var basePath = AppContext.BaseDirectory;

            var envValue =
            System.Environment.GetEnvironmentVariable("HEADLESS");

           

            var environment =
                System.Environment.GetEnvironmentVariable("ENVIRONMENT")
                ?? "QA";

            Configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(
                    "Configuration/appsettings.json",
                    optional: false,
                    reloadOnChange: false)

                .AddJsonFile(
                    $"Configuration/appsettings.{environment}.json",
                    optional: false,
                    reloadOnChange: false)
                .Build();
        }


        public static bool Headless
        {
            get
            {
                // 1. Environment variable has highest priority
                var envValue =
                    System.Environment.GetEnvironmentVariable("HEADLESS");

                if (bool.TryParse(envValue, out var envHeadless))
                    return envHeadless;

                // 2. Fall back to appsettings.QA.json
                return bool.TryParse(
                    Configuration["TestSettings:Headless"],
                    out var configHeadless)
                        ? configHeadless
                        : true;
            }
        }


        //public static bool Headless =>
        //   bool.TryParse(
        //       System.Environment.GetEnvironmentVariable("HEADLESS"),
        //       out var value)
        //       ? value
        //       : true;


        public static string Environment =>
            System.Environment.GetEnvironmentVariable("ENVIRONMENT")
            ?? "QA";

        public static string Browser =>
            System.Environment.GetEnvironmentVariable("BROWSER")
            ?? "chromium";


        //public static string Browser => Configuration["TestSettings:Browser"] ?? "chromium";

       

        //public static bool Headless => 
        //bool.TryParse(Configuration["TestSettings:Headless"], out var result) && result;


        public static string? BaseUrl => Configuration["TestSettings:BaseUrl"];

        public static string? UserName => Configuration["TestSettings:UserName"];
        public static string? Password => Configuration["TestSettings:Password"];

        public static int DefaultTimeout =>
        int.TryParse(Configuration["TestSettings:DefaultTimeout"], out var timeout)
            ? timeout
            : 30000;
    }
}