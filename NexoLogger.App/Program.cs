// See https://aka.ms/new-console-template for more information
using NexoLogger.Lib;

ILogger logger = new ConsoleLogger();
logger.LogDebug("This is a debug message.");
logger.LogInfo("This is an info message.");
logger.LogError("This is an error message.");

var longMessage = String.Concat(Enumerable.Repeat("a", 1001));
try
{
    logger.LogInfo(longMessage);
}
catch (ArgumentException ex) 
{
    logger.LogError(ex.ToString());
}

logger = new FileLogger();
logger.LogDebug("This is a debug message.");
logger.LogInfo("This is an info message.");
logger.LogError("This is an error message.");

for (int i = 1; i <= 30; i++)
{ 
    var message = $"#{i}:" + String.Concat(Enumerable.Repeat("a", 1000));
    logger.LogInfo(message);
}
