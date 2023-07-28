using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Storage.API.Services;

public interface IStorage<TObject>
{
    public Task<BaseResult> PutItemAsync(TObject item, CancellationToken cancellationToken = default);
    public Task<TObject> GetItemAsync(string itemName, CancellationToken cancellationToken = default);
    public Task<BaseResult> RemoveItemAsync(string itemName, CancellationToken cancellationToken = default);
    public Task<IEnumerable<string>> EnumerateItemNamesAsync(CancellationToken cancellationToken = default);
    public Task<BaseResult> CopyItemToBucketAsync(string itemName, string destinationBucketName,
        CancellationToken cancellationToken = default);
}