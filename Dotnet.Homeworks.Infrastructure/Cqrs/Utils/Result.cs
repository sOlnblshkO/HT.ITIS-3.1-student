namespace Dotnet.Homeworks.Infrastructure.Cqrs.Utils;

public class Result
{
    public bool IsSuccess { get; } = true;
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }

    public Result(bool isSuccessful, string? error)
    {
        IsSuccess = isSuccessful;
        if (error is not null) 
            Error = error;
    }
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? val, bool isSuccessful, string? error)
        : base(isSuccessful, error)
    {
        _value = val;
    }

    public TValue? Value => IsSuccess
        ? _value
        : throw new Exception(Error);
}