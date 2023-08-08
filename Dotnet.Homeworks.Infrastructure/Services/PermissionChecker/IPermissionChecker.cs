using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;

public interface IPermissionChecker<in TRequest>
{
    Task<PermissionResult> Validate(TRequest request);
}

