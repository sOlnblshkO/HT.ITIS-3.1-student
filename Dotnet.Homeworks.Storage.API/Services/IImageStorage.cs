using Dotnet.Homeworks.Storage.API.Dto;
using Dotnet.Homeworks.Storage.API.Dto.Internal;

namespace Dotnet.Homeworks.Storage.API.Services;

public interface IImageStorage
{
    public Task<BaseResult> PutObjectAsync(Image image, CancellationToken cancellationToken = default);
    public Task<Image> GetObjectAsync(string imageName, CancellationToken cancellationToken = default);
    public Task<BaseResult> RemoveObjectAsync(string imageName, CancellationToken cancellationToken = default);
    public Task<IEnumerable<string>> ListObjectsAsync(CancellationToken cancellationToken = default);
}