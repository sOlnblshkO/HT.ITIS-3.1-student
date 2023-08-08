using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Domain.Repositories;

public interface IUserRepository
{
    Task<IQueryable<User>> GetUsers();
    Task<User> GetUserByGuid(Guid guid);
    Task DeleteUserByGuid(Guid guid);
    Task UpdateUserByGuid(User user);
    Task<Guid> InsertUser(User user);
}