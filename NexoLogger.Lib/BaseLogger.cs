using System.Collections.Concurrent;

namespace NexoLogger.Lib
{
    public abstract class BaseLogger : ILogger
    {
        private BlockingCollection<LogItem> _logItemQueue = new BlockingCollection<LogItem>();

        protected BaseLogger() 
        {
            var thread = new Thread(() => ProcessAsyncItems());
            thread.IsBackground = true;
            thread.Start();
        }

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

            var logItem = new LogItem
            {
                Message = message,
                LogLevel = logLevel
            };

            _logItemQueue.Add(logItem);
        }

        private void ProcessAsyncItems() 
        {
            while (!_logItemQueue.IsCompleted)
            { 
                var logItem = _logItemQueue.Take();
                try 
                {
                    Log(logItem.Message, logItem.LogLevel);
                }
                catch (Exception ex) 
                {
                    Log(ex.ToString(), LogLevel.Error);
                }
            }
        }

        protected abstract void Log(string message, LogLevel logLevel);
    }
}
