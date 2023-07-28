using Dotnet.Homeworks.Storage.API.Constants;
using Dotnet.Homeworks.Storage.API.Services;
using Dotnet.Homeworks.Tests.MinioStorage.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Minio.Exceptions;

namespace Dotnet.Homeworks.Tests.MinioStorage;

[Collection(nameof(RunMinioServerInDockerFixture))]
public class MinioStorageFactoryTests
{
    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task StorageFactory_ShouldCreate_ImageStorage()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder();
        var env = testEnvBuilder.Build();
        var bucketName = Guid.NewGuid().ToString();
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(bucketName);
        Assert.True(storage is ImageStorage);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task StorageFactory_ShouldCreate_BucketForStorage()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder();
        var env = testEnvBuilder.Build();
        var bucketName = Guid.NewGuid().ToString();
        
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(bucketName);
        try
        {
            var _ = await storage.EnumerateItemNamesAsync();
        }
        catch (BucketNotFoundException e)
        {
            Assert.Fail(e.Message);
        }
        
        Assert.True(true);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task StorageFactory_ShouldCreate_BucketForPendingStorage()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder();
        var env = testEnvBuilder.Build();
        
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(Buckets.Pending);
        try
        {
            var _ = await storage.EnumerateItemNamesAsync();
        }
        catch (BucketNotFoundException e)
        {
            Assert.Fail(e.Message);
        }
        
        Assert.True(true);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public async Task StorageFactory_ShouldCreate_StorageWithEmptyUnderlyingBucket()
    {
        await using var testEnvBuilder = new MinioEnvironmentBuilder();
        var env = testEnvBuilder.Build();
        var bucketName = Guid.NewGuid().ToString();
        
        var storage = await env.StorageFactory.CreateImageStorageWithinBucketAsync(bucketName);
        var items = await storage.EnumerateItemNamesAsync();
        
        Assert.Empty(items);
    }
}