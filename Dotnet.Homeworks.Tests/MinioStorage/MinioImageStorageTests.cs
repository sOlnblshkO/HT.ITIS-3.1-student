using Dotnet.Homeworks.Storage.API.Constants;
using Dotnet.Homeworks.Tests.MinioStorage.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using static Dotnet.Homeworks.Tests.MinioStorage.Helpers.TestImage;

namespace Dotnet.Homeworks.Tests.MinioStorage;

[Collection(nameof(RunMinioServerInDockerFixture))]
public class MinioImageStorageTests
{
    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task PutItemAsync_ShouldSave_Item_Successfully()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder().WithBackgroundServicesRunOnBuild();
        var env = testEnvBuilder.Build();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Guid.NewGuid().ToString());
        var image = GetTestImage();

        var res = await storage.PutItemAsync(image);

        Assert.True(res.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task PutItemAsync_ShouldMove_ObjectToPendingBucketFirst()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder();
        var env = testEnvBuilder.Build();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Guid.NewGuid().ToString());
        var pendingStorage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Buckets.Pending);
        var image = GetTestImage();

        await storage.PutItemAsync(image);
        var fromStorage = await storage.GetItemAsync(image.FileName);
        var fromPending = await pendingStorage.GetItemAsync(image.FileName);

        Assert.Null(fromStorage);
        Assert.NotNull(fromPending);
        Assert.Equal(image, fromPending, ImagesEqual);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task PutItemAsync_ShouldNotOverwrite_ExistingItem()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder();
        var env = testEnvBuilder.Build();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Buckets.Pending);
        var image = GetTestImage();

        var result = await storage.PutItemAsync(image);
        Assert.True(result.IsSuccess);

        result = await storage.PutItemAsync(image);
        Assert.False(result.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task GetItemAsync_ShouldReturn_Item_AfterBackgroundServiceWork()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder().WithBackgroundServicesRunOnBuild();
        var env = testEnvBuilder.Build();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Guid.NewGuid().ToString());
        var image = GetTestImage();

        await storage.PutItemAsync(image);
        await env.WaitForBackgroundServiceAsync();
        var gotImage = await storage.GetItemAsync(image.FileName);

        Assert.NotNull(gotImage);
        Assert.Equal(image, gotImage, ImagesEqual);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task GetItemAsync_ShouldReturn_Null_WhenItemNotExists()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder();
        var env = testEnvBuilder.Build();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Guid.NewGuid().ToString());

        var image = await storage.GetItemAsync(Guid.NewGuid().ToString());

        Assert.Null(image);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task RemoveItemAsync_ShouldDelete_ExistingItem_Successfully()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder().WithBackgroundServicesRunOnBuild();
        var env = testEnvBuilder.Build();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Guid.NewGuid().ToString());
        var image = GetTestImage();

        await storage.PutItemAsync(image);
        await env.WaitForBackgroundServiceAsync();
        var gotImage = await storage.GetItemAsync(image.FileName);
        Assert.NotNull(gotImage);

        var res = await storage.RemoveItemAsync(image.FileName);
        Assert.True(res.IsSuccess);
        gotImage = await storage.GetItemAsync(image.FileName);
        Assert.Null(gotImage);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task RemoveItemAsync_ShouldDelete_NonExistingItem_Successfully()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder();
        var env = testEnvBuilder.Build();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Guid.NewGuid().ToString());

        var res = await storage.RemoveItemAsync(Guid.NewGuid().ToString());

        Assert.True(res.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task EnumerateItemNamesAsync_ShouldEnumerate_AllItemsCorrectly()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder().WithBackgroundServicesRunOnBuild();
        var env = testEnvBuilder.Build();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Guid.NewGuid().ToString());
        var image1 = GetTestImage();
        var image2 = GetTestImage();

        await storage.PutItemAsync(image1);
        await storage.PutItemAsync(image2);
        await env.WaitForBackgroundServiceAsync();
        var items = await storage.EnumerateItemNamesAsync();
        var itemsList = items.ToList();

        Assert.Equal(2, itemsList.Count);
        Assert.Contains(image1.FileName, itemsList);
        Assert.Contains(image2.FileName, itemsList);
    }
}