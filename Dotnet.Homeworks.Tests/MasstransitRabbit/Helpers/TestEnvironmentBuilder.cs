using Dotnet.Homeworks.Mailing.API.Consumers;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.MessagingContracts.Email;
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
    /// Should be used with caution and only for configuring services by providing Bus property.
    /// <remarks>
    /// Is not null only after SetupServices call.
    /// </remarks>
    /// </summary>
    private ITestHarness? Harness => _serviceProvider.GetTestHarness();

    public void SetupServices(Action<IServiceCollection>? configureServices = default) =>
        _serviceProvider = GetServiceProvider(configureServices);

    public void SetupProducingProcessMock(SendEmail testingEmailMessage)
    {
        if (Harness is null) SetupServices();
        var communicationServiceMock = new Mock<ICommunicationService>();
        var producerMock = new Mock<IRegistrationService>();

        // ReSharper disable 2 AsyncVoidLambda
        // Callback only accepts Action, which prohibits passing `async Task` to it (it fails in runtime)
        // This is ok as it is only used once here in callback
        communicationServiceMock.Setup(c => c.SendEmailAsync(It.Is<SendEmail>(data => data == testingEmailMessage)))
            .Callback(async () => await Harness!.Bus.Publish(testingEmailMessage));
        producerMock.Setup(p => p.RegisterAsync(It.IsAny<RegisterUserDto>()))
            .Callback(async () => await communicationServiceMock.Object.SendEmailAsync(testingEmailMessage));
        _communicationService = communicationServiceMock.Object;
        _registrationService = producerMock.Object;
    }

    public TestEnvironment Build()
    {
        _serviceProvider ??= GetServiceProvider(null);
        _communicationService ??= _serviceProvider.GetRequiredService<ICommunicationService>();
        _registrationService ??= _serviceProvider.GetRequiredService<IRegistrationService>();
        _emailConsumer ??= Harness!.GetConsumerHarness<EmailConsumer>();
        return new TestEnvironment(Harness!, _registrationService, _emailConsumer, _mailingMock);
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
    public TestEnvironment(ITestHarness harness, IRegistrationService registrationService, object emailConsumer,
        Mock<IMailingService> mailingMock)
    {
        Harness = harness;
        RegistrationService = registrationService;
        EmailConsumer = emailConsumer;
        MailingServiceMock = mailingMock;
    }

    public readonly Mock<IMailingService> MailingServiceMock;

    public readonly ITestHarness Harness;

    public readonly IRegistrationService RegistrationService;

    public readonly object EmailConsumer;
}