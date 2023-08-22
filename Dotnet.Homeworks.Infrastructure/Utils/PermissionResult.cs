using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Infrastructure.Utils;

public class PermissionResult : Result
{
    public PermissionResult(bool isSuccess, string? message = null)
        : base(isSuccess, message)
    {
    }
}