using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Infrastructure.Validation.PermissionChecker;

public interface IPermissionCheck
{
    Task<IEnumerable<PermissionResult>> CheckPermissionAsync<TRequest>(TRequest request);
}