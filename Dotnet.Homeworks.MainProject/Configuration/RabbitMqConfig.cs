namespace Dotnet.Homeworks.MainProject.Configuration;

public class RabbitMqConfig
{
    public RabbitMqConfig(string username, string password, string hostname)
    {
        Username = username;
        Password = password;
        Hostname = hostname;
    }

    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Hostname { get; private set; }
}