using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.Cqrs.UserManagement.Commands.DeleteUserByAdmin;
using Dotnet.Homeworks.Features.Cqrs.UserManagement.Queries.GetAllUsers;
using Dotnet.Homeworks.Features.Cqrs.Users.Commands.CreateUser;
using Dotnet.Homeworks.Features.Cqrs.Users.Commands.DeleteUser;
using Dotnet.Homeworks.Features.Cqrs.Users.Commands.UpdateUser;
using Dotnet.Homeworks.Features.Cqrs.Users.Queries.GetUser;

namespace Dotnet.Homeworks.Tests.CqrsValidation.Helpers;

public class TestUsers
{
    public static GetUserQuery GetUserQuery(Guid guid) => new(guid);
    
    public static DeleteUserCommand DeleteUserCommand(Guid guid) => new(guid);
    
    public static UpdateUserCommand UpdateUserCommand(User user) => new(user);
    
    public static CreateUserCommand CreateUserCommand(string name, string email) => new(name, email);

    public static GetAllUsersQuery GetAllUsersQuery() => new();

    public static DeleteUserByAdminCommand DeleteUserByAdminCommand(Guid guid) => new(guid);
}