using Dotnet.Homeworks.Storage.API.Services;
using Dotnet.Homeworks.Storage.API.ServicesExtensions;
using Dotnet.Homeworks.Tests.Shared.TestEnvironmentBuilder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MinioInstance = Dotnet.Homeworks.Tests.MinioStorage.Helpers.TestEnvironmentMinioInstance;

namespace Dotnet.Homeworks.Tests.MinioStorage.Helpers;

public class MinioEnvironmentBuilder : TestEnvironmentBuilder<MinioEnvironment>
{
    private bool _runBackgroundServicesOnBuild;

    public MinioEnvironmentBuilder WithBackgroundServicesRunOnBuild(bool runBackgroundServicesOnBuild = true)
    {
        _runBackgroundServicesOnBuild = runBackgroundServicesOnBuild;
        return this;
    }

    public override void SetupServices(Action<IServiceCollection>? configureServices = default)
    {
        var optionsConfig = Options.Create(MinioInstance.Config);
        configureServices += s => s
            .AddSingleton(optionsConfig)
            .AddMinioClient(MinioInstance.Config)
            .AddSingleton<IStorageFactory, StorageFactory>()
            .AddHostedService<PendingObjectsProcessor>();
        ServiceProvider = GetServiceProvider(configureServices);
    }

    public override MinioEnvironment Build()
    {
        if (ServiceProvider is null) SetupServices();
        var storageFactory = ServiceProvider!.GetRequiredService<IStorageFactory>();
        if (_runBackgroundServicesOnBuild) RunBackgroundServices();
        return new MinioEnvironment(storageFactory);
    }

    private void RunBackgroundServices(CancellationToken cancellationToken = default) =>
        Task.WaitAll(ServiceProvider!
            .GetServices<IHostedService>()
            .Select(s => s.StartAsync(cancellationToken))
            .ToArray(), cancellationToken);
}