using Dotnet.Homeworks.Features.RequestTypes;

namespace Dotnet.Homeworks.Features.Cqrs.Users.Commands.DeleteUser;

public class DeleteUserCommand : IClientRequest //TODO: Inherit certain interface 
{
    public Guid Guid { get; }

    public DeleteUserCommand(Guid guid)
    {
        Guid = guid;
    }
}