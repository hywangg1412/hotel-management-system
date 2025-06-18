using Microsoft.Extensions.Logging;

namespace DataAccess.Context.Common
{
    public static class AppLogger
    {
        private static ILoggerFactory _loggerFactory;
        private static ILogger _logger;

        public static void Intialize(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger("AppLogger");
        }

        public static void LogInformation(string message)
        {
            _logger?.LogInformation(message);
        }

        public static void LogError(string message)
        {
            _logger?.LogError(message);
        }

        public static void LogWarning(string message)
        {
            _logger?.LogWarning(message);
        }

        public static void LogDebug(string message)
        {
            _logger?.LogDebug(message);
        }

        public static void LogError(Exception ex, string message)
        {
            _logger?.LogError($"Error: {ex.Message}");
        }
    }
}