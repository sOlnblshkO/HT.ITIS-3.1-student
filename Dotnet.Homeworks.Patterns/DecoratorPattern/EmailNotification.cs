namespace Dotnet.Homeworks.Patterns.DecoratorPattern;

public class EmailNotification : INotification
{
    public void Send(string message)
    {
        Console.WriteLine("Messages sent");
    }
}