using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.MainProject.Services;
using MassTransit.Testing;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;

public class MasstransitEnvironment
{
    public MasstransitEnvironment(ITestHarness harness, IRegistrationService registrationService, object emailConsumer,
        IMailingService mailingMock)
    {
        Harness = harness;
        RegistrationService = registrationService;
        EmailConsumer = emailConsumer;
        MailingServiceMock = mailingMock;
    }

    public IMailingService MailingServiceMock { get; }

    public ITestHarness Harness { get; }

    public IRegistrationService RegistrationService { get; }

    public object EmailConsumer { get; }
}