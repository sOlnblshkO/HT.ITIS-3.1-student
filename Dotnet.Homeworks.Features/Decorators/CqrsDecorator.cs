using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Decorators;

public class CqrsDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>    
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{

    protected CqrsDecorator() : base()
    {
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();  // Декоратор вызывает отнаследованный метод родителя: await base.Handle(request, cancellationToken);
}