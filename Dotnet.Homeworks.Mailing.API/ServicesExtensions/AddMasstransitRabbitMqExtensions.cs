using Dotnet.Homeworks.Mailing.API.Configuration;
using Microsoft.Extensions.Options;

namespace Dotnet.Homeworks.Mailing.API.ServicesExtensions;

public static class AddMasstransitRabbitMqExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        IOptions<RabbitMqConfig> rabbitConfiguration)
    {
        throw new NotImplementedException();
    }
}