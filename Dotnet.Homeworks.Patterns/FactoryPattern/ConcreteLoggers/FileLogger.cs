namespace Dotnet.Homeworks.Patterns.FactoryPattern.ConcreteLoggers;

public class FileLogger : ILogger
{
    private readonly string _path;

    public FileLogger(string path)
    {
        _path = path;
    }

    public void Log(string message)
    {
        File.AppendAllText(_path, message);
    }
}