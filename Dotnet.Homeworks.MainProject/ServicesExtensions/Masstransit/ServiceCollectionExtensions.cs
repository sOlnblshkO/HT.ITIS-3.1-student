using Dotnet.Homeworks.MainProject.Configuration;
using MassTransit;

namespace Dotnet.Homeworks.MainProject.ServicesExtensions.Masstransit;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(
        this IServiceCollection services,
        RabbitMqConfig rabbitConfiguration)
    {
        services.AddMassTransit(busConf =>
        {
            busConf.SetKebabCaseEndpointNameFormatter();
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