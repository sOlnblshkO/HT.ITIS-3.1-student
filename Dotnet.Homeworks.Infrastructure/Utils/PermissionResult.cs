namespace Dotnet.Homeworks.Infrastructure.Utils;

public class PermissionResult
{
    public bool IsSuccess { get; }
    public string Message { get; }
    
    public PermissionResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}