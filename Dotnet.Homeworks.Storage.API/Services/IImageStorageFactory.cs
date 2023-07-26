namespace Dotnet.Homeworks.Storage.API.Services;

public interface IImageStorageFactory
{
    public Task<IImageStorage> CreateWithinBucketAsync(string bucketName);
}