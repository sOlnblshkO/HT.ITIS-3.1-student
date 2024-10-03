namespace Dotnet.Homeworks.Patterns.FactoryPattern;

public abstract class LoggerCreator
{
    public abstract ILogger CreateLogger();

    public void LogMessage(string message)
    {
        var logger = CreateLogger();
        logger.Log(message);
    }
}