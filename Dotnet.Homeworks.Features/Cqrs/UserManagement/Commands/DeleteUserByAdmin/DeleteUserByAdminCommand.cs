namespace Dotnet.Homeworks.Features.UserManagement.Commands.DeleteUserByAdmin;

public class DeleteUserByAdminCommand 
{
    public Guid Guid { get; }

    public DeleteUserByAdminCommand(Guid guid)
    {
        Guid = guid;
    }
}