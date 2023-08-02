using Dotnet.Homeworks.Mailing.API.Consumers;
using Dotnet.Homeworks.Mailing.API.Helpers;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using Dotnet.Homeworks.Tests.RunLogic.Utils.TestEnvironmentBuilder;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;

public class MasstransitEnvironmentBuilder : TestEnvironmentBuilder<MasstransitEnvironment>
{
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
    private ITestHarness? Harness => ServiceProvider?.GetTestHarness();

    public override void SetupServices(Action<IServiceCollection>? configureServices = default)
    {
        var assembly = AssemblyReference.Assembly;
        configureServices ??= _ => { };
        configureServices += s => s
            .AddSingleton(_mailingMock.Object)
            .AddSingleton<ICommunicationService, CommunicationService>()
            .AddMassTransitTestHarness(b => b.AddConsumers(assembly));
        ServiceProvider = GetServiceProvider(configureServices);
    }

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

    public override MasstransitEnvironment Build()
    {
        if (ServiceProvider is null) SetupServices();
        _communicationService ??= ServiceProvider!.GetRequiredService<ICommunicationService>();
        _registrationService ??= ServiceProvider!.GetRequiredService<IRegistrationService>();
        _emailConsumer ??= Harness!.GetConsumerHarness<EmailConsumer>();
        return new MasstransitEnvironment(Harness!, _registrationService, _emailConsumer, _mailingMock);
    }
}