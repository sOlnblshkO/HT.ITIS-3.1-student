using Dotnet.Homeworks.Infrastructure.Cqrs.Decorators.Common;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;
using FluentValidation;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Behaviors.Validation;

public class ValidationBehavior<TRequest,TResponse> : CqrsDecoratorBase<TRequest,TResponse>, IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();
        
        var error = _validators
            .Select(async validator => await validator.ValidateAsync(request))
            .SelectMany(result => result.Result.Errors)
            .FirstOrDefault(failure => failure is not null)?.ErrorMessage;

        if (error is null)
            return await next();
        
        return await GenerateFailedResult<TResponse>(error);
    }
}