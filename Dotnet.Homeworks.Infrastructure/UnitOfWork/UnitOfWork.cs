using Dotnet.Homeworks.Data.DatabaseContext;

namespace Dotnet.Homeworks.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _appDbContext;

    public UnitOfWork(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task SaveChangesAsync(CancellationToken token = default)
    {
        await _appDbContext.SaveChangesAsync(token);
    }
}