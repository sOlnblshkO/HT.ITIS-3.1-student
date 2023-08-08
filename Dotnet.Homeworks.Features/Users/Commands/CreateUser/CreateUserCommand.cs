using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Features.Users.Commands.CreateUser;

public class CreateUserCommand : ICommand<CreateUserDto>
{
    public string Name { get; }
    public string Email { get; }

    public CreateUserCommand(string name, string email)
    {
        Name = name;
        Email = email;
    }

    public Result<CreateUserDto> Result { get; init; }
}