using Dotnet.Homeworks.Storage.API.Configuration;
using Microsoft.Extensions.Options;

namespace Dotnet.Homeworks.Storage.API.ServicesExtensions;

public static class AddMinioExtensions
{
    public static IServiceCollection AddMinioClient(this IServiceCollection services,
        MinioConfig minioConfiguration)
    {
        throw new NotImplementedException();
    }
}