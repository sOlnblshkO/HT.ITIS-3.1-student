namespace Dotnet.Homeworks.Features.Users.Commands.CreateUser;

public class CreateUserCommand //TODO: Inherit certain interface 
{
    public string Name { get; }
    public string Email { get; }

    public CreateUserCommand(string name, string email)
    {
        Name = name;
        Email = email;
    }
}