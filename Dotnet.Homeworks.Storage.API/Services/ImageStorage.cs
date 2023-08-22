using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Storage.API.Dto.Internal;

namespace Dotnet.Homeworks.Storage.API.Services;

public class ImageStorage : IStorage<Image>
{
    public Task<Result> PutItemAsync(Image item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Image?> GetItemAsync(string itemName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RemoveItemAsync(string itemName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> EnumerateItemNamesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result> CopyItemToBucketAsync(string itemName, string destinationBucketName,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}