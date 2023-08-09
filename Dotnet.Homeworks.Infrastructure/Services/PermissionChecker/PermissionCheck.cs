using System.Reflection;
using System.Reflection.Metadata;
using Dotnet.Homeworks.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore.Internal;

namespace Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;

public class PermissionCheck : IPermissionCheck
{
    private static Dictionary<Type, Type> CheckersByCommandTypes { get; set; } = new();
    private readonly IServiceProvider _serviceProvider;

    public PermissionCheck(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public static void RegisterCheckersFromAssembly(Assembly assembly)
    {
        var scanResults = Scan(assembly); 
        
        RegisterCheckers(scanResults);
    }

    public IEnumerable<PermissionResult> CheckPermission<TRequest>(TRequest request)
    {
        List<Type> checkerTypes = new List<Type>();
        var inheritedTypes = typeof(TRequest).GetInterfaces().ToList();
        inheritedTypes.Add(typeof(TRequest));
        
        foreach (var type in inheritedTypes)
        {
            Type checkerType;
            if (CheckersByCommandTypes.TryGetValue(type, out checkerType))
                checkerTypes.Add(checkerType);
        }

        var checker = checkerTypes.Select(GetCheckerInstance)
                .Select(async x=> await ((dynamic)x).Validate(request))
                .Select(x=>(PermissionResult)x.Result);

        return checker;
    }
    
    private static IEnumerable<ScanResult> Scan(Assembly assembly)
    {
        var result = assembly.GetTypes()   
            .Select(x => new ScanResult(
                x.GetInterfaces().FirstOrDefault(inter =>
                    inter.Name == typeof(IPermissionChecker<>).Name)?.GetGenericArguments()[0],
                x
            ))
            .Where(x => x.CommandType is not null);

        return result;
    }

    private static void RegisterCheckers(IEnumerable<ScanResult> scanResults)
    {
        foreach (var scanResult in scanResults)
        {
            CheckersByCommandTypes[scanResult.CommandType]
                = scanResult.CheckerType;
        }
    }

    private object GetCheckerInstance(Type handlerType)
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
    
    private record ScanResult(Type? CommandType, Type CheckerType);
}
