using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Mediator.Helpers;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] handlersAssemblies)
    {
        Mediator.RegisterHandlersFromAssembly(handlersAssemblies);
        services.AddTransient<IMediator, Mediator>();
        return services;
    }
}