using Dotnet.Homeworks.Features.RequestTypes;

namespace Dotnet.Homeworks.Features.Cqrs.UserManagement.Commands.DeleteUserByAdmin;

public class DeleteUserByAdminCommand : IAdminRequest
{
    public Guid Guid { get; }

    public DeleteUserByAdminCommand(Guid guid)
    {
        Guid = guid;
    }
}