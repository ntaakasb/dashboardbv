using System;

namespace OsCoreApplication.Logger
{
    public static class OsLog
    {
        private static ILogger _logger;

        public static void Instance()
        {
            _logger = new Log();
        }

        public static void Fatal(string message, Exception ex = null) => _logger?.Fatal(message, ex);

        public static void Error(string message, Exception ex = null) => _logger.Error(message, ex);

        public static void Warn(string message, Exception ex = null) => _logger.Warn(message, ex);

        public static void Info(string message, Exception ex = null) => _logger.Info(message, ex);

        public static void Debug(string message, Exception ex = null) => _logger.Debug(message, ex);
    }
}
