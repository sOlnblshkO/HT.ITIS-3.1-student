using Dotnet.Homeworks.Infrastructure.Cqrs.Decorators.PermissionCheck;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;
using FluentValidation;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Decorators.Validation;

public abstract class ValidationDecorator<TRequest, TResponse> : PermissionCheckDecorator<TRequest, TResponse>, IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    protected ValidationDecorator(
        IEnumerable<IValidator<TRequest>> validators,
        IPermissionCheck checker
        ) 
        : base(checker)
    {
        _validators = validators;
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await base.Handle(request, cancellationToken);
        
        var error = _validators
            .Select(async validator => await validator.ValidateAsync(request))
            .SelectMany(result => result.Result.Errors)
            .FirstOrDefault(failure => failure is not null)?.ErrorMessage;

        if (error is null)
            return await base.Handle(request, cancellationToken);
        
        return await GenerateFailedResult<TResponse>(error);
    }
}