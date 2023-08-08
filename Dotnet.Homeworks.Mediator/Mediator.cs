using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Mediator;

public partial class Mediator : IMediator
{
    private static readonly Dictionary<Type, Type> RequestHandlerMappings = new();
    private readonly IServiceProvider _serviceProvider;
    
    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        => SendGeneric<IRequest<TResponse>, TResponse>(request, cancellationToken);

    public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IRequest => SendGeneric<TRequest, Task>(request, cancellationToken);

    public async Task<object?> Send(object request, CancellationToken cancellationToken = default)
    {
        if (request is null) throw new ArgumentNullException(nameof(request)); 
        var requestType = request.GetType();
        var requestInterfaceType = requestType.GetInterfaces()
            .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IRequest<>));
        if (requestInterfaceType is null)
        {
            requestInterfaceType = requestType.GetInterfaces().FirstOrDefault(t => t == typeof(IRequest));
            if (requestInterfaceType is null)
                throw new ArgumentException($"{requestType.Name} does not implement {nameof(IRequest)}",
                    nameof(request));
            
            return Send((IRequest)request, cancellationToken)
                .ContinueWith(_ => Task.FromResult<object?>(null), cancellationToken).Unwrap();
        }

        var sendMethod =
            typeof(Dotnet.Homeworks.Mediator.Mediator).GetMethods().First(m =>
            {
                if (m.Name != "Send") return false;
                var p = m.GetParameters();
                return p.Length == 2 && p[0].ParameterType.GUID == typeof(IRequest<>).GUID
                                     && p[1].ParameterType.GUID == typeof(CancellationToken).GUID;
            });
        sendMethod = sendMethod.MakeGenericMethod(requestInterfaceType);
        var res = sendMethod.Invoke(this, new[] { request, cancellationToken })!;
        var t = await ((dynamic)res).ConfigureAwait(false);
        return (object)t;
    }

    private async Task<TResponse> SendGeneric<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
    {
        if (request is null) throw new ArgumentNullException(nameof(request));
        var requestType = request.GetType();
        Type handlerType;
        if (!RequestHandlerMappings.TryGetValue(requestType as dynamic, out handlerType))
            throw new InvalidOperationException($"No handler registered for {requestType.Name}");
        var handlerInstance = GetRequestHandlerInstance(handlerType);
        var handleMethod = handlerType.GetMethod("Handle", new[] { requestType, typeof(CancellationToken) });
        if (handleMethod?.ReturnType.GetGenericArguments()[0] != typeof(TResponse))
            throw new InvalidOperationException(
                $"Method Handle of {handlerType.Name} didn't return {typeof(TResponse).FullName} but {handleMethod?.ReturnType?.FullName}");
        
        var invokeMethodDelegate = new RequestHandlerDelegate<TResponse>(
            async () => await ((Task<TResponse>) ( handleMethod?.Invoke(handlerInstance, new object[] { request, cancellationToken })!)).ConfigureAwait(false)
        );

        var pipelineDelegate = ConfigurePipelineBehavior(invokeMethodDelegate, (dynamic)request, cancellationToken);
        var result = await pipelineDelegate.Invoke();
        return result;
    }

    private RequestHandlerDelegate<TResponse> ConfigurePipelineBehavior<TRequest, TResponse>(
        RequestHandlerDelegate<TResponse> handle, 
        TRequest request,
        CancellationToken cancellationToken
        )
    {
        var typesForPipeline = new Type[]{ request.GetType(), typeof(TResponse) };
        var behaviors = _serviceProvider
            .GetServices(typeof(IPipelineBehavior<,>).MakeGenericType(typesForPipeline))
            .Select(x=>(IPipelineBehavior<TRequest, TResponse>)x!)
            .ToArray();

        if (!behaviors.Any())
            return handle;
        
        var result = new RequestHandlerDelegate<TResponse>( async () => 
            await behaviors[behaviors.Length-1].Handle(request, handle, cancellationToken));
        
        for (int i = behaviors.Length - 2; i >= 0; i--)
        {
            var result1 = result;
            var behavior = behaviors[i];
            result = async () =>
            {
                return await behavior.Handle(request, result1, cancellationToken);
            };
        }

        return result;
        
    }
    
    private object GetRequestHandlerInstance(Type handlerType)
    {
        var constructor = handlerType.GetConstructors().Single();
        var parameters = constructor.GetParameters();
        var parameterValues = new object[parameters.Length];

        for (var i = 0; i < parameters.Length; i++)
        {
            var dependencyType = parameters[i].ParameterType;
            var dependencyInstance = _serviceProvider.GetService(dependencyType);
            parameterValues[i] = dependencyInstance ??
                                 throw new InvalidOperationException(
                                     $"Dependency with required type {dependencyType} is not registered");
        }

        return constructor.Invoke(parameterValues);
    }
}