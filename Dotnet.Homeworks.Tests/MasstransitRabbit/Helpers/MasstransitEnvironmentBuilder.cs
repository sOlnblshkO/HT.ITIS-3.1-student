using Dotnet.Homeworks.Mailing.API.Consumers;
using Dotnet.Homeworks.Mailing.API.Helpers;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using Dotnet.Homeworks.Tests.Shared.TestEnvironmentBuilder;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;

public class MasstransitEnvironmentBuilder : TestEnvironmentBuilder<MasstransitEnvironment>
{
    private readonly IMailingService _mailingMock = Substitute.For<IMailingService>();
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
            .AddSingleton(_mailingMock)
            .AddSingleton<ICommunicationService, CommunicationService>()
            .AddMassTransitTestHarness(b => b.AddConsumers(assembly));
        ServiceProvider = GetServiceProvider(configureServices);
    }

    public void SetupProducingProcessMock(SendEmail testingEmailMessage)
    {
        if (Harness is null) SetupServices();
    
        var communicationServiceSubstitute = Substitute.For<ICommunicationService>();
        var producerSubstitute = Substitute.For<IRegistrationService>();

        communicationServiceSubstitute.SendEmailAsync(Arg.Is<SendEmail>(data => data == testingEmailMessage), Arg.Any<CancellationToken>())
            .Returns(_ => Harness!.Bus.Publish(testingEmailMessage));

        producerSubstitute.RegisterAsync(Arg.Any<RegisterUserDto>(), Arg.Any<CancellationToken>())
            .Returns(_ => communicationServiceSubstitute.SendEmailAsync(testingEmailMessage, Arg.Any<CancellationToken>()));

        _communicationService = communicationServiceSubstitute;
        _registrationService = producerSubstitute;
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