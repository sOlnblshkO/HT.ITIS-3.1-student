using Dotnet.Homeworks.Storage.API.Services;

namespace Dotnet.Homeworks.Tests.MinioStorage.Helpers;

public class MinioEnvironment
{
    private readonly TimeSpan _pendingObjectsProcessorPeriod;
    
    public MinioEnvironment(IStorageFactory storageFactory, TimeSpan pendingObjectsProcessorPeriod)
    {
        StorageFactory = storageFactory;
        _pendingObjectsProcessorPeriod = pendingObjectsProcessorPeriod;
    }

    public IStorageFactory StorageFactory  { get; }
    public async Task WaitForBackgroundServiceAsync() => await Task.Delay(_pendingObjectsProcessorPeriod);
}