using Dotnet.Homeworks.Storage.API.Dto.Internal;

namespace Dotnet.Homeworks.Storage.API.Services;

public class StorageFactory : IStorageFactory
{
    public Task<IStorage<Image>> CreateImageStorageWithinBucketAsync(string bucketName)
    {
        // TODO: implement creation of IImageStorage with the given bucketName
        // e.g. storage should somehow know the bucket to save objects
        throw new NotImplementedException();
    }
}