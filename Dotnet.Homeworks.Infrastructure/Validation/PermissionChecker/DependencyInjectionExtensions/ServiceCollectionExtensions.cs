using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Infrastructure.Validation.PermissionChecker.DependencyInjectionExtensions;

public static class ServiceCollectionExtensions
{
    public static void AddPermissionChecks(
        this IServiceCollection serviceCollection,
        Assembly assembly
    )
    {
        throw new NotImplementedException();
    }
    
    public static void AddPermissionChecks(
        this IServiceCollection serviceCollection,
        Assembly[] assemblies
    )
    {
        throw new NotImplementedException();
    }
}