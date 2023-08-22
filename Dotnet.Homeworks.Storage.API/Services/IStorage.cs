using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Storage.API.Services;

public interface IStorage<TObject>
{
    public Task<Result> PutItemAsync(TObject item, CancellationToken cancellationToken = default);
    public Task<TObject?> GetItemAsync(string itemName, CancellationToken cancellationToken = default);
    public Task<Result> RemoveItemAsync(string itemName, CancellationToken cancellationToken = default);
    public Task<IEnumerable<string>> EnumerateItemNamesAsync(CancellationToken cancellationToken = default);
    public Task<Result> CopyItemToBucketAsync(string itemName, string destinationBucketName,
        CancellationToken cancellationToken = default);
}