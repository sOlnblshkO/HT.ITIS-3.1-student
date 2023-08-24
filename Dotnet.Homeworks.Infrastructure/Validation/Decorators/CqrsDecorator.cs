namespace Dotnet.Homeworks.Infrastructure.Validation.Decorators;

public class CqrsDecorator<TRequest, TResponse> //TODO: Inherit certain interface 
{
    protected CqrsDecorator() : base()
    {
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();  //TODO: Decorator invoke parent's method: await base.Handle(request, cancellationToken);
}