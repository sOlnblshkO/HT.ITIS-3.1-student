using Dotnet.Homeworks.Data.DatabaseContext;

namespace Dotnet.Homeworks.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task SaveChangesAsync(CancellationToken token) => _dbContext.SaveChangesAsync(token);
}