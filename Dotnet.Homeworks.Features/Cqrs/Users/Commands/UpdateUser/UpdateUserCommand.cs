using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.RequestTypes;

namespace Dotnet.Homeworks.Features.Cqrs.Users.Commands.UpdateUser;

public class UpdateUserCommand : IClientRequest 
{
    public User User { get; }
    
    public Guid Guid { get; }

    public UpdateUserCommand(User user)
    {
        Guid = user.Id;
        User = user;
    }

}