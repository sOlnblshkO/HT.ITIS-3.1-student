namespace Dotnet.Homeworks.Application.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken token = default);
}