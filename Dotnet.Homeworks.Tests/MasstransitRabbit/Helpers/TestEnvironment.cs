using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.MainProject.Services;
using MassTransit.Testing;
using Moq;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;

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

    public Mock<IMailingService> MailingServiceMock { get; }

    public ITestHarness Harness { get; }

    public IRegistrationService RegistrationService { get; }

    public object EmailConsumer { get; }
}