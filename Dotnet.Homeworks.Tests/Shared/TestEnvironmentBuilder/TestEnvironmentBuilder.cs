using System.Reflection;
using Dotnet.Homeworks.MainProject.ServicesExtensions.Mapper;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Tests.Shared.TestEnvironmentBuilder;

public abstract class TestEnvironmentBuilder<T> : IAsyncDisposable
{
    protected ServiceProvider? ServiceProvider;

    protected static ServiceProvider GetServiceProvider(Action<IServiceCollection>? configureServices = default)
    {
        var serviceCollection = new ServiceCollection();
        configureServices?.Invoke(serviceCollection);

        if (IsHomeworkInProgressOrComplete(RunLogic.Homeworks.AutoMapper))
            serviceCollection.AddMappers(Features.Helpers.AssemblyReference.Assembly);

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
}