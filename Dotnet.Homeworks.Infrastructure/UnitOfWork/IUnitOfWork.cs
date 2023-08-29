namespace Dotnet.Homeworks.Infrastructure.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken token);
}