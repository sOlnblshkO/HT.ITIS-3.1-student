using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    public Task<IQueryable<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByGuidAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserByGuidAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> InsertUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}