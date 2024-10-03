using Dotnet.Homeworks.Patterns.FactoryPattern.ConcreteLoggers;

namespace Dotnet.Homeworks.Patterns.FactoryPattern.ConcreteCreators;

public class FileLogCreator : LoggerCreator
{
    private readonly string _path;

    public FileLogCreator(string path)
    {
        _path = path;
    }

    public override ILogger CreateLogger()
    {
        return new FileLogger(_path);
    }
}