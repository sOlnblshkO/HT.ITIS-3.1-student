using Dotnet.Homeworks.Mailing.API.Consumers;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.MainProject.Services;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;

internal class TestEnvironmentBuilder : IAsyncDisposable
{
    private ServiceProvider? _serviceProvider;
    private readonly Mock<IMailingService> _mailingMock = new();
    private ICommunicationService? _communicationService;
    private IRegistrationService? _registrationService;
    private object? _emailConsumer;
    
    /// <summary>
    /// Should be used with caution only for configuring services by providing Bus property
    /// </summary>
    public ITestHarness? Harness { get; private set; }

    public void SetupServices(Action<IServiceCollection>? configureServices = default) =>
        _serviceProvider = GetServiceProvider(configureServices);

    public void AddCommunicationService(ICommunicationService communicationService) =>
        _communicationService = communicationService;

    public void AddRegistrationProducer(IRegistrationService registrationService) =>
        _registrationService = registrationService;

    public TestEnvironment Build()
    {
        _serviceProvider ??= GetServiceProvider(null);
        Harness ??= _serviceProvider.GetTestHarness();
        _communicationService ??= _serviceProvider.GetRequiredService<ICommunicationService>();
        _registrationService ??= _serviceProvider.GetRequiredService<IRegistrationService>();
        _emailConsumer ??= Harness.GetConsumerHarness<EmailConsumer>();
        return new TestEnvironment(Harness, _communicationService, _registrationService, _emailConsumer, _mailingMock);
    }

    private ServiceProvider GetServiceProvider(Action<IServiceCollection>? configureServices)
    {
        var assembly = typeof(EmailConsumer).Assembly;

        var serviceCollection = new ServiceCollection()
            .AddSingleton(_mailingMock.Object)
            .AddSingleton<ICommunicationService, CommunicationService>()
            .AddMassTransitTestHarness(b => b.AddConsumers(assembly));
        configureServices?.Invoke(serviceCollection);
        return serviceCollection.BuildServiceProvider();
    }

    public ValueTask DisposeAsync()
    {
        return _serviceProvider?.DisposeAsync() ?? ValueTask.CompletedTask;
    }
}

public class TestEnvironment
{
    public TestEnvironment(ITestHarness harness, ICommunicationService communicationService,
        IRegistrationService registrationService, object emailConsumer,
        Mock<IMailingService> mailingMock)
    {
        Harness = harness;
        CommunicationService = communicationService;
        RegistrationService = registrationService;
        EmailConsumer = emailConsumer;
        MailingServiceMock = mailingMock;
    }

    public readonly Mock<IMailingService> MailingServiceMock;

    public readonly ITestHarness Harness;

    public readonly ICommunicationService CommunicationService;

    public readonly IRegistrationService RegistrationService;

    public readonly object EmailConsumer;
}