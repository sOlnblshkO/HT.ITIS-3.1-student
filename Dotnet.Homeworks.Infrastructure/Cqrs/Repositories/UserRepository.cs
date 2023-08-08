using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IQueryable<User>> GetUsers()
    {
        return _dbContext.Users;
    }

    public async Task<User?> GetUserByGuid(Guid guid)
    {
        return _dbContext.Users.FirstOrDefault(user => user.Id == guid);
    }

    public async Task DeleteUserByGuid(Guid guid)
    {
        await _dbContext.Users.Where(user=>user.Id==guid).ExecuteDeleteAsync();
    }

    public async Task UpdateUserByGuid(User user)
    {
        await _dbContext.Users
            .Where(x => x.Id == user.Id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(x => x.Name, user.Name)  
                .SetProperty(x => x.Email, user.Email)
            );
    }

    public async Task<Guid> InsertUser(User user)
    {
        var trackedEntity = await _dbContext.Users.AddAsync(user);
        return trackedEntity.Entity.Id;
    }
}