using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Helpers;
using MassTransit;

namespace Dotnet.Homeworks.Mailing.API.ServicesExtensions;

public static class AddMasstransitRabbitMqExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(
        this IServiceCollection services,
        RabbitMqConfig rabbitConfiguration)
    {
        services.AddMassTransit(busConf =>
        {
            busConf.SetKebabCaseEndpointNameFormatter();
            busConf.AddConsumers(AssemblyReference.Assembly);
            busConf.UsingRabbitMq((context, bus) =>
            {
                bus.Host(new Uri($"rabbitmq://{rabbitConfiguration.Hostname}"), conf =>
                {
                    conf.Username(rabbitConfiguration.Username);
                    conf.Password(rabbitConfiguration.Password);
                });
                
                bus.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}