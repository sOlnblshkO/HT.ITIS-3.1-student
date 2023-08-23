using System.Reflection;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Tests.Shared.TestEnvironmentBuilder;

public abstract class TestEnvironmentBuilder<T> : IAsyncDisposable
{
    protected ServiceProvider? ServiceProvider;

    protected static ServiceProvider GetServiceProvider(Action<IServiceCollection>? configureServices = default)
    {
        var serviceCollection = new ServiceCollection();
        configureServices?.Invoke(serviceCollection);

        if (!IsHomeworkInProgressOrComplete(RunLogic.Homeworks.AutoMapper)) 
            return serviceCollection.BuildServiceProvider();
        
        var types = ScanMappers(Features.Helpers.AssemblyReference.Assembly);
        foreach (var type in types)
        {
            serviceCollection.AddTransient(type.Item2, type.Item1);
        }

        return serviceCollection.BuildServiceProvider();
    }

    public abstract void SetupServices(Action<IServiceCollection>? configureServices = default);

    public abstract T Build();

    public virtual ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ServiceProvider?.DisposeAsync() ?? ValueTask.CompletedTask;
    }
    
    protected static bool IsHomeworkInProgressOrComplete(RunLogic.Homeworks homework)
    {
        var attrHomeworkProgress =
            typeof(HomeworkAttribute).Assembly.GetCustomAttributes<HomeworkProgressAttribute>().Single();
        var isCqrsComplete = attrHomeworkProgress.Number >= (int) homework;
        return isCqrsComplete;
    }

    private static IEnumerable<(Type, Type)> ScanMappers(Assembly assembly)
    {
        var result = assembly.GetTypes()
            .Select(type =>
            (
                Impl: type,
                Inter: type.GetInterfaces()
                    .FirstOrDefault(inter => inter.GetCustomAttributes().Any(x=>x is MapperAttribute))
            ))
            .Where(x => x.Inter is not null);

        return result;
    }
}