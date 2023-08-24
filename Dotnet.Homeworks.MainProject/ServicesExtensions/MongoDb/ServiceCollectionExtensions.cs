using Dotnet.Homeworks.MainProject.Configuration;
using Microsoft.Extensions.Options;

namespace Dotnet.Homeworks.MainProject.ServicesExtensions.MongoDb;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoClient(this IServiceCollection services,
        IOptions<MongoDbConfig> mongoConfiguration)
    {
        throw new NotImplementedException();
    }
}