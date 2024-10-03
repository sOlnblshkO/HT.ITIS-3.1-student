namespace Dotnet.Homeworks.Patterns.FactoryPattern.ConcreteLoggers;

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"Console logger: {message}");
    }
}