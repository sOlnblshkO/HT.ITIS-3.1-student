using Dotnet.Homeworks.Features.RequestTypes;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

namespace Dotnet.Homeworks.Features.Users.Commands.DeleteUser;

public class DeleteUserCommand : ICommand, IClientRequest
{
    public Guid Guid { get; }

    public DeleteUserCommand(Guid guid)
    {
        Guid = guid;
    }
}