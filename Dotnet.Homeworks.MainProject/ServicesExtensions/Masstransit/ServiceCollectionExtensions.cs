using Dotnet.Homeworks.MainProject.Configuration;
using Microsoft.Extensions.Options;

namespace Dotnet.Homeworks.MainProject.ServicesExtensions.Masstransit;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        IOptions<RabbitMqConfig> rabbitConfiguration)
    {
        throw new NotImplementedException();
    }
}