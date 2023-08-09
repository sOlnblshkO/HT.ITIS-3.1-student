using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.RequestTypes;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Features.Users.Commands.UpdateUser;

public class UpdateUserCommand : ICommand, IClientRequest
{
    public Guid Guid { get; }
    public string Name { get; }
    public string Email { get; }
    public User User { get; }

    public UpdateUserCommand(Guid guid, string name, string email)
    {
        Guid = guid;
        Name = name;
        Email = email;
        User = new User() { Email = Email, Id = Guid, Name = Name };
    }

    public UpdateUserCommand(User user)
    {
        Guid = user.Id;
        Name = user.Name;
        Email = user.Email;
        User = user;
    }
    
    public Result Result { get; set; }
}