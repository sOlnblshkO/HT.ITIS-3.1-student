using Dotnet.Homeworks.Storage.API.Dto;
using Dotnet.Homeworks.Storage.API.Dto.Internal;

namespace Dotnet.Homeworks.Storage.API.Services;

public class ImageStorage : IImageStorage
{
    public Task<BaseResult> PutObjectAsync(Image image, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Image> GetObjectAsync(string imageName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResult> RemoveObjectAsync(string imageName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<string>> ListObjectsAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}