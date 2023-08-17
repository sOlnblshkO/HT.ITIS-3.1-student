using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Tests.RunLogic.Utils.TestEnvironmentBuilder;

public abstract class TestEnvironmentBuilder<T> : IAsyncDisposable
{
    protected ServiceProvider? ServiceProvider;

    protected static ServiceProvider GetServiceProvider(Action<IServiceCollection>? configureServices = default)
    {
        var serviceCollection = new ServiceCollection();
        configureServices?.Invoke(serviceCollection);
        return serviceCollection.BuildServiceProvider();
    }

    public abstract void SetupServices(Action<IServiceCollection>? configureServices = default);

    public abstract T Build();

    public virtual ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ServiceProvider?.DisposeAsync() ?? ValueTask.CompletedTask;
    }
}