// See https://aka.ms/new-console-template for more information
using NexoLogger.Lib;

ILogger logger = new ConsoleLogger();
logger.LogDebug("This is a debug message.");
logger.LogInfo("This is an info message.");
logger.LogError("This is an error message.");

var longMessage = String.Concat(Enumerable.Repeat("a", 1001));
logger.LogInfo(longMessage);
