using Dotnet.Homeworks.Storage.API.Constants;
using Dotnet.Homeworks.Tests.MinioStorage.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using static Dotnet.Homeworks.Tests.MinioStorage.Helpers.TestImage;

namespace Dotnet.Homeworks.Tests.MinioStorage;

[Collection(nameof(RunMinioServerInDockerFixture))]
public class MinioPendingObjectsProcessorTests
{
    [Homework(RunLogic.Homeworks.MinioStorage, true)]
    public async Task ShouldClear_PendingBucket()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder().WithBackgroundServicesRunOnBuild();
        var env = testEnvBuilder.Build();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Guid.NewGuid().ToString());
        var pendingStorage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Buckets.Pending);
        var image = GetTestImage();

        await storage.PutItemAsync(image);
        await MinioEnvironment.WaitForBackgroundServiceAsync();
        var items = await pendingStorage.EnumerateItemNamesAsync();

        Assert.Empty(items);
    }

    [Homework(RunLogic.Homeworks.MinioStorage, true)]
    public async Task ShouldMove_ObjectsToBuckets()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder().WithBackgroundServicesRunOnBuild();
        var env = testEnvBuilder.Build();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Guid.NewGuid().ToString());
        var pendingStorage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Buckets.Pending);
        var image = GetTestImage();

        await storage.PutItemAsync(image);
        var itemsInStorageBucket = await storage.EnumerateItemNamesAsync();
        var itemsInPendingBucket = await pendingStorage.EnumerateItemNamesAsync();

        Assert.Empty(itemsInStorageBucket);
        Assert.Contains(image.FileName, itemsInPendingBucket);
        await MinioEnvironment.WaitForBackgroundServiceAsync();

        itemsInStorageBucket = await storage.EnumerateItemNamesAsync();
        itemsInPendingBucket = await pendingStorage.EnumerateItemNamesAsync();

        Assert.Contains(image.FileName, itemsInStorageBucket);
        Assert.Empty(itemsInPendingBucket);
    }
}