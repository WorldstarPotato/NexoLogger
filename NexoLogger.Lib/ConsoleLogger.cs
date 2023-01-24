namespace NexoLogger.Lib
{
    public class ConsoleLogger : BaseLogger
    {
        protected override void Log(string message, LogLevel logLevel)
        {
            if (message == null || message.Length > 1000)
                throw new ArgumentException("The message cannot be null and cannot be longer than 1000 characters.", nameof(message));

            var consoleColor = GetConsoleColorByLogLevel(logLevel);
            Console.ForegroundColor = consoleColor;

            Console.WriteLine(GetFormattedMessage(message, logLevel));
            Console.ResetColor();
        }

        private ConsoleColor GetConsoleColorByLogLevel(LogLevel logLevel)
        {
            switch(logLevel)
            {
                case LogLevel.Debug:
                    return ConsoleColor.Gray;
                case LogLevel.Info:
                    return ConsoleColor.Green;
                case LogLevel.Error:
                    return ConsoleColor.Red;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel));
            }
        }
    }
}
