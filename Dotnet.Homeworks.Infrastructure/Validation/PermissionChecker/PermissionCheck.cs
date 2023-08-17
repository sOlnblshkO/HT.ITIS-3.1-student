using Dotnet.Homeworks.Infrastructure.Utils;

namespace Dotnet.Homeworks.Infrastructure.Validation.PermissionChecker;

public class PermissionCheck : IPermissionCheck
{
    public async Task<IEnumerable<PermissionResult>> CheckPermissionAsync<TRequest>(TRequest request)
    {
        throw new NotImplementedException();
    }
}
