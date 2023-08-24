namespace Dotnet.Homeworks.Shared.Dto;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string? Error { get; }

    public Result(bool isSuccessful, string? error = default)
    {
        IsSuccess = isSuccessful;
        if (error is not null) 
            Error = error;
    }
}

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? val, bool isSuccessful, string? error = default)
        : base(isSuccessful, error)
    {
        _value = val;
    }

    public TValue? Value => IsSuccess
        ? _value
        : throw new Exception(Error);
}