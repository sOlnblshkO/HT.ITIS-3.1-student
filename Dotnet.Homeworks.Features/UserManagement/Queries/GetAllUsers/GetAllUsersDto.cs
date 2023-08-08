namespace Dotnet.Homeworks.Features.UserManagement.Queries.GetAllUsers;

public record GetAllUsersDto (
    Guid Guid,
    string Name,
    string Email
    );