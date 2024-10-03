namespace Dotnet.Homeworks.Patterns.MediatorPattern;

public interface IChatRoomMediator
{
    public void ShowMessage(User user, string message);
}