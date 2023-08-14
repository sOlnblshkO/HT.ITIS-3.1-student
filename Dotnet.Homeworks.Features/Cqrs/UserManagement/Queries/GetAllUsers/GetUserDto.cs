namespace Dotnet.Homeworks.Features.Cqrs.UserManagement.Queries.GetAllUsers;

public record GetUserDto (
    Guid Guid, 
    string Name, 
    string Email
    );