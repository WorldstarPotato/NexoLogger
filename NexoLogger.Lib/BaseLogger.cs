namespace NexoLogger.Lib
{
    public abstract class BaseLogger : ILogger
    {
        protected string GetFormattedMessage(string message, LogLevel logLevel)
        {
            return GetFormattedMessage(message, logLevel, DateTime.Now);
        }

        protected string GetFormattedMessage(string message, LogLevel logLevel, DateTime logTime)
        {
            return $"{logTime:yyyy-MM-dd HH:mm:ss.fff} {"[" + logLevel + "]",-8} {message}";
        }

        public void LogDebug(string message)
        {
            Log(message, LogLevel.Debug);
        }

        public void LogError(string message)
        {
            Log(message, LogLevel.Error);
        }

        public void LogInfo(string message)
        {
            Log(message, LogLevel.Info);
        }

        protected abstract void Log(string message, LogLevel logLevel);
    }
}
