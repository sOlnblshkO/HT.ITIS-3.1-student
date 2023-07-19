using Dotnet.Homeworks.Mailing.API.Consumers;
using Dotnet.Homeworks.Mailing.API.Dto;
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

public class MasstransitConsumerTests
{
    private readonly Mock<IMailingService> _mailingMock = new();

    private ServiceProvider GetServiceProvider(Action<IServiceCollection>? configureServices)
    {
        var serviceCollection = new ServiceCollection()
            .AddSingleton(_mailingMock.Object)
            .AddSingleton<ICommunicationService, CommunicationService>()
            .AddMassTransitTestHarness(b =>
            {
                b.AddConsumer<EmailConsumer>();
            });
        configureServices?.Invoke(serviceCollection);
        return serviceCollection.BuildServiceProvider();
    }
    
    [Homework(RunLogic.Homeworks.Rabbit)]
    public async Task MailingConsumer_ShouldConsume_CorrectMessages_WithoutErrors()
    {
        await using var provider = GetServiceProvider(null);
        var harness = provider.GetRequiredService<ITestHarness>();
        var communicationServiceMock = new Mock<ICommunicationService>();
        var testSendEmail = new SendEmail("test", "test", "test", "test");
        var producerMock = new Mock<IRegistrationService>();
        var consumer = harness.GetConsumerHarness<EmailConsumer>();
        
        communicationServiceMock.Setup(c => c.SendEmailAsync(It.Is<SendEmail>(data => data == testSendEmail)))
            .Callback(async () => await harness.Bus.Publish(testSendEmail));
        producerMock.Setup(p => p.RegisterAsync(It.IsAny<RegisterUserDto>()))
            .Callback(async () => await communicationServiceMock.Object.SendEmailAsync(testSendEmail));

        try
        {
            await harness.Start();
            await producerMock.Object.RegisterAsync(new RegisterUserDto("", ""));

            Assert.True(await consumer.Consumed.Any<SendEmail>(message => message.Context.Message == testSendEmail));
            Assert.False(await consumer.Consumed.Any<SendEmail>(message => message.Exception is not null));
        }
        finally
        {
            await harness.Stop();
        }
    }
    
    [Homework(RunLogic.Homeworks.Rabbit)]
    public async Task MailingConsumer_ShouldConsume_Messages_SentFromRegisterService_WithoutErrors()
    {
        await using var provider = GetServiceProvider(null);
        var harness = provider.GetRequiredService<ITestHarness>();
        var communicationService = provider.GetRequiredService<ICommunicationService>();
        var producer = new RegistrationService(communicationService);
        var consumer = harness.GetConsumerHarness<EmailConsumer>();

        try
        {
            await harness.Start();
            await producer.RegisterAsync(new RegisterUserDto("", ""));
            
            Assert.True(await consumer.Consumed.Any<SendEmail>());
            Assert.False(await consumer.Consumed.Any<SendEmail>(message => message.Exception is not null));
        }
        finally
        {
            await harness.Stop();
        }
    }
    
    [Homework(RunLogic.Homeworks.Rabbit)]
    public async Task MailingConsumer_ShouldConsume_OneMessage_WhenOneMessageSentFromRegisterService_WithoutErrors()
    {
        await using var provider = GetServiceProvider(null);
        var harness = provider.GetRequiredService<ITestHarness>();
        var communicationService = provider.GetRequiredService<ICommunicationService>();
        var producer = new RegistrationService(communicationService);
        var consumer = harness.GetConsumerHarness<EmailConsumer>();

        try
        {
            await harness.Start();
            await producer.RegisterAsync(new RegisterUserDto("", ""));
            
            Assert.True(await consumer.Consumed.Any<SendEmail>());
            Assert.True(consumer.Consumed.Count() == 1);
            Assert.False(await consumer.Consumed.Any<SendEmail>(message => message.Exception is not null));
        }
        finally
        {
            await harness.Stop();
        }
    }
    
    [Homework(RunLogic.Homeworks.Rabbit)]
    public async Task MailingConsumer_ShouldCall_IMailingService_Once()
    {
        _mailingMock.Invocations.Clear();
        await using var provider = GetServiceProvider(null);
        var harness = provider.GetRequiredService<ITestHarness>();
        var communicationServiceMock = new Mock<ICommunicationService>();
        var testSendEmail = new SendEmail("test", "test", "test", "test");
        var producerMock = new Mock<IRegistrationService>();
        
        communicationServiceMock.Setup(c => c.SendEmailAsync(It.Is<SendEmail>(data => data == testSendEmail)))
            .Callback(async () => await harness.Bus.Publish(testSendEmail));
        producerMock.Setup(p => p.RegisterAsync(It.IsAny<RegisterUserDto>()))
            .Callback(async () => await communicationServiceMock.Object.SendEmailAsync(testSendEmail));

        try
        {
            await harness.Start();
            await producerMock.Object.RegisterAsync(new RegisterUserDto("", ""));
            await Task.Delay(100);

            _mailingMock.Verify(m => m.SendEmailAsync(It.IsAny<EmailMessage>()), Times.Once);
        }
        finally
        {
            await harness.Stop();
        }
    }
}