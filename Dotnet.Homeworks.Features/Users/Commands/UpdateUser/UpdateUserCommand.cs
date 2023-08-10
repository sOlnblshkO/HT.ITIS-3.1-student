using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.RequestTypes;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

namespace Dotnet.Homeworks.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : ICommand, IClientRequest
{
    public Guid Guid { get; }
    public User User { get; }

    public UpdateUserCommand(User user)
    {
        Guid = user.Id;
        User = user;
    }
}