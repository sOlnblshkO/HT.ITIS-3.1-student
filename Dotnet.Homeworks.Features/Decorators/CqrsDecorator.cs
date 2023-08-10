namespace Dotnet.Homeworks.Features.Decorators;

public class CqrsDecorator<TRequest, TResponse> // : ???
{
    protected CqrsDecorator() : base()
    {
    }

    public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        => throw new NotImplementedException();  // Декоратор вызывает отнаследованный метод родителя: await base.Handle(request, cancellationToken);
}