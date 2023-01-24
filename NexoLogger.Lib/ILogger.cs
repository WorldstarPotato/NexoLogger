namespace NexoLogger.Lib
{
    public interface ILogger
    {
        public void LogDebug(string message);
        public void LogInfo(string message);
        public void LogError(string message);
    }
}
