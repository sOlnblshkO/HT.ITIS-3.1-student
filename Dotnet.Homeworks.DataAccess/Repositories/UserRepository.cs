using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    public Task<IQueryable<User>> GetUsersAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Guid> InsertUserAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}