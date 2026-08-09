using Microsoft.Playwright;
using Serilog;

namespace DCB.Framework.Utilities
{
    public static class LoggerManager
    {
        public static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Information()
            .WriteTo.Console(outputTemplate:
            "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} ){NewLine}{Exception}")

   
            .WriteTo.File(
        Path.Combine(AppContext.BaseDirectory, "Logs", "log-.txt"),
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 10,
        outputTemplate:
            "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}){NewLine}{Exception}")
        .CreateLogger();
        }


        public static void CloseLogger()
        {
            Log.CloseAndFlush();
        }
    }
}