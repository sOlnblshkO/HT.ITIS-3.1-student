using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Mediator.DependencyInjectionExtensions;


public static class ServiceCollectionExtensions
{
    //TODO: Register your custom mediator
    public static IServiceCollection AddMediator(this IServiceCollection services, params Assembly[] handlersAssemblies)
    {
        throw new NotImplementedException(); 
    }
}