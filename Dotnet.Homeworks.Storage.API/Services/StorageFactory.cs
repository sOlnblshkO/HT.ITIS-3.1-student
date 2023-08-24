using Dotnet.Homeworks.Storage.API.Dto.Internal;

namespace Dotnet.Homeworks.Storage.API.Services;

public class StorageFactory : IStorageFactory
{
    public Task<IStorage<Image>> CreateImageStorageWithinBucketAsync(string bucketName)
    {
        // TODO: implement creation of IImageStorage with the given bucketName
        // e.g. each storage should work only within its bucket (but still may copy items to another bucket)
        throw new NotImplementedException();
    }
}