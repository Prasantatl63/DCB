
using Serilog;

namespace Framework.Utilities.Logging
{
    public static class LoggerManager
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
                return;

            var logDirectory = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Logs");

            Directory.CreateDirectory(logDirectory);

            var logFile = Path.Combine(
                logDirectory,
                "TestExecution-.log");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    logFile,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    shared: true,
                    outputTemplate:
                        "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] " +
                        "[{Level:u3}] " +
                        "{Message:lj}{NewLine}" +
                        "{Exception}")
                .CreateLogger();

            _initialized = true;

            Log.Information("======================================");
            Log.Information("Test execution logging initialized");
            Log.Information("Log directory: {LogDirectory}", logDirectory);
            Log.Information("======================================");
        }

        public static void Close()
        {
            if (!_initialized)
                return;

            Log.Information("Test execution logging stopped");

            Log.CloseAndFlush();

            _initialized = false;
        }
    }
}

