using Dotnet.Homeworks.Infrastructure.Cqrs.Decorators.Common;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Behaviors.PermissionCheck;

public class PermissionCheckBehavior<TRequest,TResponse> : CqrsDecoratorBase<TRequest,TResponse> , IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IPermissionCheck _checker;

    public PermissionCheckBehavior(IPermissionCheck checker)
    {
        _checker = checker;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var error = _checker.CheckPermission(request);
    
        if (error.All(x=>x.IsSuccess))
            return await next();
        
        return await GenerateFailedResult<TResponse>(error.FirstOrDefault().Message ?? "");
    }
}