using Dotnet.Homeworks.Mailing.API.Consumers;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.MessagingContracts.Email;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Dotnet.Homeworks.Tests.Masstransit;

public class MasstransitProducersTests
{
    private readonly Mock<IMailingService> _mailingMock = new();

    private ServiceProvider GetServiceProvider(Action<IServiceCollection>? configureServices)
    {
        var assembly = typeof(EmailConsumer).Assembly;
        
        var serviceCollection = new ServiceCollection()
            .AddSingleton(_mailingMock.Object)
            .AddSingleton<ICommunicationService, CommunicationService>()
            .AddMassTransitTestHarness(b =>
            {
                b.AddConsumers(assembly);
            });
        configureServices?.Invoke(serviceCollection);
        return serviceCollection.BuildServiceProvider();
    }
    
    [Homework(RunLogic.Homeworks.Rabbit)]
    public async Task RegisterService_ShouldPublishOrSend_SendEmail_WithoutErrors()
    {
        await using var provider = GetServiceProvider(null);
        var harness = provider.GetRequiredService<ITestHarness>();
        var communicationService = provider.GetRequiredService<ICommunicationService>();
        var producer = new RegistrationService(communicationService);

        try
        {
            await harness.Start();
            await producer.RegisterAsync(new RegisterUserDto("", ""));
            
            Assert.True(await harness.Published.Any<SendEmail>() || await harness.Sent.Any<SendEmail>());
            Assert.False(await harness.Published.Any<SendEmail>(p => p.Exception is not null) ||
                         await harness.Sent.Any<SendEmail>(p => p.Exception is not null));
        }
        finally
        {
            await harness.Stop();
        }
    }
}