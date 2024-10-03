using Dotnet.Homeworks.Patterns.FactoryPattern.ConcreteLoggers;

namespace Dotnet.Homeworks.Patterns.FactoryPattern.ConcreteCreators;

public class ConsoleLogCreator : LoggerCreator
{
    public override ILogger CreateLogger()
    {
        return new ConsoleLogger();
    }
}