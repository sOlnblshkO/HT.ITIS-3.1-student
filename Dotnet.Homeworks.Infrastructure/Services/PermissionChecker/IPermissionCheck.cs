using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;

public interface IPermissionCheck
{
    Task<IEnumerable<PermissionResult>> CheckPermissionAsync<TRequest>(TRequest request);
}