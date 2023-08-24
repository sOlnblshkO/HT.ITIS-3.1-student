namespace Dotnet.Homeworks.Features.UserManagement.Queries.GetAllUsers;

public record GetAllUsersDto (
    IEnumerable<GetUserDto> Users
    );