namespace Dotnet.Homeworks.Infrastructure.Utils;

public class PermissionResult : Result
{
    public bool IsSuccess { get; }
    public string Message { get; }
    
    public PermissionResult(bool isSuccess, string? message = null)
        : base(isSuccess, message)
    {
    }
}