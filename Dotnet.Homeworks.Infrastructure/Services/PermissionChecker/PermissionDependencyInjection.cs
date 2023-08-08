using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;

public static class PermissionDependencyInjection
{
    public static void AddPermissionChecks(
        this IServiceCollection serviceCollection,
        Assembly assembly
    )
    {
        PermissionCheck.RegisterCheckersFromAssembly(assembly);
        serviceCollection.AddTransient<IPermissionCheck, PermissionCheck>();
    }
}