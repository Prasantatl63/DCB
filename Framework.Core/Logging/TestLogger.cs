using Serilog;

namespace DCB.Framework.Logging;

public static class TestLogger
{
    public static void Initialize()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File(
                Path.Combine(
                    AppContext.BaseDirectory,
                    "Logs",
                    "test-.log"),
                rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    public static void Info(
        string message,
        params object[] args)
    {
        Log.Information(message, args);
    }

    public static void Debug(
        string message,
        params object[] args)
    {
        Log.Debug(message, args);
    }

    public static void Warning(
        string message,
        params object[] args)
    {
        Log.Warning(message, args);
    }

    public static void Error(
        Exception exception,
        string message,
        params object[] args)
    {
        Log.Error(exception, message, args);
    }

    public static void Close()
    {
        Log.CloseAndFlush();
    }
}