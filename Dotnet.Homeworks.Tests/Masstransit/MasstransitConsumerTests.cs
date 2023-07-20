using Dotnet.Homeworks.Mailing.API.Consumers;
using Dotnet.Homeworks.Mailing.API.Dto;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.MessagingContracts.Email;
using Dotnet.Homeworks.Tests.Masstransit.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using static Dotnet.Homeworks.Tests.Masstransit.Helpers.ReflectionHelpers;
// ReSharper disable AsyncVoidLambda

namespace Dotnet.Homeworks.Tests.Masstransit;

public class MasstransitConsumerTests
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
            var anyCorrectMessagesConsumed =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(consumer,
                    message => message.Context.Message == testSendEmail);
            var anyCorrectMessagesConsumedWithErrors =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(consumer,
                    message => message.Exception is not null);
            
            Assert.True(anyCorrectMessagesConsumed);
            Assert.False(anyCorrectMessagesConsumedWithErrors);
        }
        finally
        {
            await harness.Stop();
        }
    }

    [Homework(RunLogic.Homeworks.Rabbit)]
    public async Task MailingConsumer_ShouldConsume_CorrectMessages_WithoutErrors_Reflection()
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
            var anyCorrectMessagesConsumed =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(consumer,
                    message => message.Context.Message == testSendEmail);
            var anyCorrectMessagesConsumedWithErrors =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(consumer,
                    message => message.Exception is not null);
            
            Assert.True(anyCorrectMessagesConsumed);
            Assert.False(anyCorrectMessagesConsumedWithErrors);
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
            var anyConsumed = await AnyConsumedMessagesWithFilterAsync<SendEmail>(consumer);
            var anyConsumedWithErrors =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(consumer, message => message.Exception is not null);

            Assert.True(anyConsumed);
            Assert.False(anyConsumedWithErrors);
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
            var anyConsumed = await AnyConsumedMessagesWithFilterAsync<SendEmail>(consumer);
            var anyConsumedWithErrors =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(consumer, message => message.Exception is not null);
            var countConsumedMessages =CountConsumedMessages<SendEmail>(consumer);

            Assert.True(anyConsumed);
            Assert.Equal(1, countConsumedMessages);
            Assert.False(anyConsumedWithErrors);
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