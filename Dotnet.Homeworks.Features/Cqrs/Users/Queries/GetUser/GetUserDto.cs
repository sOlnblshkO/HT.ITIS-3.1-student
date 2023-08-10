namespace Dotnet.Homeworks.Features.Cqrs.Users.Queries.GetUser;

public record GetUserDto (
    Guid Guid, 
    string Name, 
    string Email
    );