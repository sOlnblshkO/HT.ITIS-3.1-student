namespace Dotnet.Homeworks.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken token = default)
    {
        throw new NotImplementedException();
    }
}