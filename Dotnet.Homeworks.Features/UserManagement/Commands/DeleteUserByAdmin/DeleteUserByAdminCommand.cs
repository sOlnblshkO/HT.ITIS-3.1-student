using Dotnet.Homeworks.Infrastructure.Validation.RequestTypes;

namespace Dotnet.Homeworks.Features.UserManagement.Commands.DeleteUserByAdmin;

public class DeleteUserByAdminCommand : IAdminRequest //TODO: Inherit certain interface 
{
    public Guid Guid { get; }

    public DeleteUserByAdminCommand(Guid guid)
    {
        Guid = guid;
    }
}