namespace Dotnet.Homeworks.Patterns.DecoratorPattern;

public class SMSNotificationDecorator : NotificationDecorator
{
    public SMSNotificationDecorator(INotification notification)
        : base(notification)
    {
    }

    public override void Send(string message)
    {
        _notification.Send(message); // Отправляем исходное уведомление
        Console.WriteLine($"Sending SMS: {message}");
    }
}