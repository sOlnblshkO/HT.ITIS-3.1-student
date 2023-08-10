using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand
{
    public Guid Guid { get; }
    public User User { get; }

    public UpdateUserCommand(User user)
    {
        Guid = user.Id;
        User = user;
    }
}