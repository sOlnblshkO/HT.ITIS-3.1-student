namespace Dotnet.Homeworks.Features.Cqrs.Users.Commands.CreateUser;

public class CreateUserCommand 
{
    public string Name { get; }
    public string Email { get; }

    public CreateUserCommand(string name, string email)
    {
        Name = name;
        Email = email;
    }
}