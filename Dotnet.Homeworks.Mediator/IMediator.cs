namespace Dotnet.Homeworks.Mediator;

public interface IMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);

    Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest;
    
    /// <summary>
    /// If there's no matching handlers for this type of request, it returns null. Otherwise, it should be either
    /// <code>IRequest parametrized by TResponse</code> or <code>IRequest</code>
    /// </summary>
    Task<dynamic?> Send(dynamic request, CancellationToken cancellationToken = default);
}