using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;

public interface IPermissionCheck
{
    IEnumerable<PermissionResult> CheckPermission<TRequest>(TRequest request);

    Task<IEnumerable<PermissionResult>> CheckPermissionAsync<TRequest>(TRequest request);
}