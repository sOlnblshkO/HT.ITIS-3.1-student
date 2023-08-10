namespace Dotnet.Homeworks.Features.Users.Queries.GetUser;

public record GetUserDto (
    Guid Guid, 
    string Name, 
    string Email
    );