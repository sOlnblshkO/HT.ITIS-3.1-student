using Dotnet.Homeworks.MainProject.Configuration;
using Microsoft.Extensions.Options;

namespace Dotnet.Homeworks.MainProject.ServicesExtensions.OpenTelemetry;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services,
        IOptions<OpenTelemetryConfig> openTelemetryConfiguration)
    {
        throw new NotImplementedException();
    }
}