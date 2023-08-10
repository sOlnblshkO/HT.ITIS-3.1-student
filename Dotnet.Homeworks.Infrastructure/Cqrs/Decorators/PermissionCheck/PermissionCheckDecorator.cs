using Dotnet.Homeworks.Infrastructure.Cqrs.Decorators.Common;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Decorators.PermissionCheck;

public class PermissionCheckDecorator<TRequest, TResponse> : CqrsDecoratorBase<TRequest, TResponse>, IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IPermissionCheck _checker;

    public PermissionCheckDecorator(
        IPermissionCheck checker
        )
    {
        _checker = checker;
    }
    
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var error = _checker.CheckPermissionAsync(request);
            
        if (error.All(x=>x.IsSuccess))
            return await GenerateSucceedResult<TResponse>();
        
        return await GenerateFailedResult<TResponse>(error.Where(x=>!x.IsSuccess).FirstOrDefault().Message ?? "");
    }
}