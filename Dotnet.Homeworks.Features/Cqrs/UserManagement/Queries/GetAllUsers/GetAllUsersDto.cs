namespace Dotnet.Homeworks.Features.Cqrs.UserManagement.Queries.GetAllUsers;

public record GetAllUsersDto (
    Guid Guid,
    string Name,
    string Email
    );