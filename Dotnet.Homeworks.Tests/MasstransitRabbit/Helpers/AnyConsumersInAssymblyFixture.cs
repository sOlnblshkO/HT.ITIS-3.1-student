using System.Reflection;
using Dotnet.Homeworks.Mailing.API.Consumers;
using MassTransit;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;

public class AnyConsumersInAssemblyFixture : IDisposable, ICollectionFixture<AnyConsumersInAssemblyFixture>
{
    private static readonly Assembly Assembly = typeof(IEmailConsumer).Assembly;
    
    public AnyConsumersInAssemblyFixture()
    {
        if (!IsAnyConsumerInAssembly)
            throw new NoConsumersInAssemblyException(Assembly.ToString());
    }

    private static bool IsAnyConsumerInAssembly => Assembly
        .GetTypes().Any(t => t.GetInterfaces().Contains(typeof(IConsumer)));

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

[CollectionDefinition(nameof(AnyConsumersInAssemblyFixture))]
public class AnyConsumersInAssemblyCollection : ICollectionFixture<AnyConsumersInAssemblyFixture>
{
    // This class has no code in it; its purpose is to define the collection
}