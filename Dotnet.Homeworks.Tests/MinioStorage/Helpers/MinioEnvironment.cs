using Dotnet.Homeworks.Storage.API.Constants;
using Dotnet.Homeworks.Storage.API.Services;

namespace Dotnet.Homeworks.Tests.MinioStorage.Helpers;

public class MinioEnvironment
{
    public MinioEnvironment(IStorageFactory storageFactory)
    {
        StorageFactory = storageFactory;
    }

    public IStorageFactory StorageFactory { get; }
    public static async Task WaitForBackgroundServiceAsync() => await Task.Delay(PendingObjectProcessor.Period * 1.1);
}