using Dotnet.Homeworks.Storage.API.Dto.Internal;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Storage.API.Services;

public class ImageStorage : IStorage<Image>
{
    public Task<BaseResult> PutItemAsync(Image item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Image> GetItemAsync(string itemName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> RemoveItemAsync(string itemName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> EnumerateItemNamesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> CopyItemToBucketAsync(string itemName, string destinationBucketName,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}