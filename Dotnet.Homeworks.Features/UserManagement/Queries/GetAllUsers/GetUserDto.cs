namespace Dotnet.Homeworks.Features.UserManagement.Queries.GetAllUsers;

public record GetUserDto (
    Guid Guid, 
    string Name, 
    string Email
    );