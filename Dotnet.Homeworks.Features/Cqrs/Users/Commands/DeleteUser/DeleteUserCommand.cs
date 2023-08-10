namespace Dotnet.Homeworks.Features.Users.Commands.DeleteUser;

public class DeleteUserCommand
{
    public Guid Guid { get; }

    public DeleteUserCommand(Guid guid)
    {
        Guid = guid;
    }
}