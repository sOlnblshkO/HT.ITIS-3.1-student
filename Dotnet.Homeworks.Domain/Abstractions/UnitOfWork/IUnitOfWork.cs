namespace Dotnet.Homeworks.Domain.Abstractions.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken token = default);
}