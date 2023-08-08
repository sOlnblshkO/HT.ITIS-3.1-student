using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Decorators.Common;

public abstract class CqrsDecoratorBase<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse : Result 
{
    private protected static async Task<TResponse> GenerateFailedResult<TResponse>(string error)
        where TResponse : Result
    {
        return await GenerateResult<TResponse>(false, error);
    }
    
    private protected static async Task<TResponse> GenerateSucceedResult<TResponse>()
        where TResponse : Result
    {
        return await GenerateResult<TResponse>(true, null);
    }

    private async static Task<TResponse> GenerateResult<TResponse>(bool isSuccess, string? error)
        where TResponse : Result
    {
        if (typeof(TResponse) == typeof(Result))
            return (new Result(isSuccess, error) as TResponse)!;
        
        var valueType = typeof(TResponse).GenericTypeArguments[0];
        var resultGeneric = typeof(Result<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(valueType)
            .GetConstructor( new[] { valueType, typeof(bool), typeof(string)})!
            .Invoke(new object?[] { null, isSuccess, error });

        return (TResponse)resultGeneric!;
    }

    public bool IsResponseGeneric<TResponse>() => typeof(TResponse).IsGenericType;
}