namespace Dotnet.Homeworks.Patterns.MediatorPattern;

public class ChatRoom : IChatRoomMediator
{
    public void ShowMessage(User user, string message)
    {
        Console.WriteLine($"Sender {user?.Name}");
    }
}