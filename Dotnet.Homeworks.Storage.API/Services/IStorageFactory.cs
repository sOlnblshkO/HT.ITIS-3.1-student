using Dotnet.Homeworks.Storage.API.Dto.Internal;

namespace Dotnet.Homeworks.Storage.API.Services;

public interface IStorageFactory
{
    public Task<IStorage<Image>> CreateImageStorageWithinBucketAsync(string bucketName);
}