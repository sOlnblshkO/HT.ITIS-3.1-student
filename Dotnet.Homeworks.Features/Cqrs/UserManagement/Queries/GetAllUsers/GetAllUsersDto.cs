namespace Dotnet.Homeworks.Features.Cqrs.UserManagement.Queries.GetAllUsers;

public record GetAllUsersDto (
    IEnumerable<GetUserDto> Users
    );