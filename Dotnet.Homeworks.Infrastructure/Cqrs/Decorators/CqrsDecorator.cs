using Dotnet.Homeworks.Infrastructure.Cqrs.Decorators.Validation;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;
using FluentValidation;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Decorators;

public class CqrsDecorator<TRequest, TResponse> : ValidationDecorator<TRequest, TResponse>,
    IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{

    protected CqrsDecorator(
        IEnumerable<IValidator<TRequest>> validators,
        IPermissionCheck checker
    )
        : base(validators, checker)
    {
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        => await base.Handle(request, cancellationToken);
}