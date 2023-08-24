using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Domain.Abstractions.Repositories;

public interface IUserRepository
{
    Task<IQueryable<User>> GetUsersAsync(CancellationToken cancellationToken);
    Task<User?> GetUserByGuidAsync(Guid guid, CancellationToken cancellationToken);
    Task DeleteUserByGuidAsync(Guid guid, CancellationToken cancellationToken);
    Task UpdateUserAsync(User user, CancellationToken cancellationToken);
    Task<Guid> InsertUserAsync(User user, CancellationToken cancellationToken);
}