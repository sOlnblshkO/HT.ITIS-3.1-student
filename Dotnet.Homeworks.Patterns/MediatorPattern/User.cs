namespace Dotnet.Homeworks.Patterns.MediatorPattern;

public class User
{
    private readonly IChatRoomMediator _chatRoomMediator;
    
    public User(string name, IChatRoomMediator chatRoomMediator)
    {
        Name = name;
        _chatRoomMediator = chatRoomMediator;
    }

    /// <summary>
    /// Имя
    /// </summary>
    public string? Name { get; set; }

    public void SendMessage(string message) => _chatRoomMediator.ShowMessage(this, message);
}