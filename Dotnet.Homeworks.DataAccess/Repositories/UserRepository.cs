using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    public async Task<IQueryable<User>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserByGuidAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUserByGuidAsync(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> InsertUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}