using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Domain.Repositories;

public interface IUserRepository
{
    Task<IQueryable<User>> GetUsersAsync();
    Task<User> GetUserByGuidAsync(Guid guid);
    Task DeleteUserByGuidAsync(Guid guid);
    Task UpdateUserAsync(User user);
    Task<Guid> InsertUserAsync(User user);
}