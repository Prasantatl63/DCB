
using Serilog;

namespace Framework.Utilities.Logging
{
    public static class TestLogger
    {
        public static void Info(string message, params object[] args)
        {
            Log.Information(message, args);
        }

        public static void Debug(string message,params object[] args)
        {
            Log.Debug(message, args);
        }

        public static void Warning(string message,params object[] args)
        {
            Log.Warning(message, args);
        }

        public static void Error(Exception exception,string message,params object[] args)
        {
            Log.Error(exception, message, args);
        }
    }
}

