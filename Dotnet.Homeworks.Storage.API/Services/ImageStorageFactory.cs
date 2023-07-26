namespace Dotnet.Homeworks.Storage.API.Services;

public class ImageStorageFactory : IImageStorageFactory
{
    public Task<IImageStorage> CreateWithinBucketAsync(string bucketName)
    {
        // TODO: implement creation of IImageStorage with the given bucketName
        // e.g. storage should somehow know the bucket to save objects
        throw new NotImplementedException();
    }
}