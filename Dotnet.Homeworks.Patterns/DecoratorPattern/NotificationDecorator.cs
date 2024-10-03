namespace Dotnet.Homeworks.Patterns.DecoratorPattern;

public abstract class NotificationDecorator : INotification
{
    protected INotification _notification;

    public NotificationDecorator(INotification notification)
    {
        _notification = notification;
    }
    
    public abstract void Send(string message);
}