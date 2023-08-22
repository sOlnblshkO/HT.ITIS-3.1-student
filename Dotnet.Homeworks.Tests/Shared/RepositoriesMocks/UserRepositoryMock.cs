using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Tests.Shared.RepositoriesMocks;

public class UserRepositoryMock : IUserRepository
{
    private readonly Dictionary<Guid, User> _users = new();

    public Task<IQueryable<User>> GetUsersAsync()
    {
        var users = _users.Values.AsQueryable();
        return Task.FromResult(users);
    }

    public Task<User?> GetUserByGuidAsync(Guid guid)
    {
        _users.TryGetValue(guid, out var value);
        return Task.FromResult(value);
    }

    public Task DeleteUserByGuidAsync(Guid guid)
    {
        _users.Remove(guid);
        return Task.CompletedTask;
    }

    public Task UpdateUserAsync(User user)
    {
        if (_users.ContainsKey(user.Id))
            _users[user.Id] = user;

        return Task.CompletedTask;
    }

    public Task<Guid> InsertUserAsync(User user)
    {
        return _users.TryAdd(user.Id, user) ? Task.FromResult(user.Id) : Task.FromException<Guid>(new Exception(""));
    }
}