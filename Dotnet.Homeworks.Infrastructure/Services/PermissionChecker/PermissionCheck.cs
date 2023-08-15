using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;

public class PermissionCheck : IPermissionCheck
{
    public IEnumerable<PermissionResult> CheckPermission<TRequest>(TRequest request)
    {
        throw new NotImplementedException();
    }
    
    public async Task<IEnumerable<PermissionResult>> CheckPermissionAsync<TRequest>(TRequest request)
    {
        throw new NotImplementedException();
    }
}
