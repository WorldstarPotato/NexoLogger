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
            LogWithCheck(message, LogLevel.Debug);
        }

        public void LogError(string message)
        {
            LogWithCheck(message, LogLevel.Error);
        }

        public void LogInfo(string message)
        {
            LogWithCheck(message, LogLevel.Info);
        }

        private void LogWithCheck(string message, LogLevel logLevel) 
        {
            if (message == null)
                throw new ArgumentException("The message cannot be null.", nameof(message));

            Log(message, logLevel);
        }

        protected abstract void Log(string message, LogLevel logLevel);
    }
}
