using Dotnet.Homeworks.Features.RequestTypes;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : ICommand, IClientRequest
{
    public Guid Guid { get; }
    public string Name { get; }
    public string Email { get; }

    public UpdateUserCommand(Guid guid, string name, string email)
    {
        Guid = guid;
        Name = name;
        Email = email;
    }
    public Result Result { get; set; }
}