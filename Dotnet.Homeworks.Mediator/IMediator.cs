namespace Dotnet.Homeworks.Mediator;

public interface IMediator
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest;
    Task<dynamic?> Send(dynamic request, CancellationToken cancellationToken = default);
}