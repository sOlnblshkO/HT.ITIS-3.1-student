namespace Dotnet.Homeworks.Patterns.DecoratorPattern;

public class PushNotificationDecorator : NotificationDecorator
{
    public PushNotificationDecorator(INotification notification)
        : base(notification)
    {
    }

    public override void Send(string message)
    {
        _notification.Send(message);
    }
}