namespace Dotnet.Homeworks.Infrastructure.Utils;

public class PermissionResult
{
    public bool IsSuccess { get; }
    public string Message { get; }
    
    public PermissionResult(bool isSuccess, string? message = null)
    {
        IsSuccess = isSuccess;
        if (message is not null) 
            Message = message;
    }
}