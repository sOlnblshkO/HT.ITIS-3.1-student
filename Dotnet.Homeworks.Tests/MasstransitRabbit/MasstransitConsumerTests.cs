using Dotnet.Homeworks.Mailing.API.Dto;
using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.MessagingContracts.Email;
using Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using static Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers.ReflectionHelpers;
// ReSharper disable AsyncVoidLambda
// ReSharper disable AccessToDisposedClosure

namespace Dotnet.Homeworks.Tests.MasstransitRabbit;

public class MasstransitConsumerTests
{
    [Homework(RunLogic.Homeworks.MasstransitRabbit)]
    public async Task MailingConsumer_ShouldConsume_CorrectMessages_WithoutErrors()
    {
        await using var testEnv = new TestEnvironmentBuilder();
        testEnv.SetupServices();

        #region Mocking ICommunicationService and IRegistrationService
        
        var communicationServiceMock = new Mock<ICommunicationService>();
        var producerMock = new Mock<IRegistrationService>();
        var testSendEmail = new SendEmail("test", "test", "test", "test");
        
        communicationServiceMock.Setup(c => c.SendEmailAsync(It.Is<SendEmail>(data => data == testSendEmail)))
            .Callback(async () => await testEnv.Harness!.Bus.Publish(testSendEmail));
        producerMock.Setup(p => p.RegisterAsync(It.IsAny<RegisterUserDto>()))
            .Callback(async () => await communicationServiceMock.Object.SendEmailAsync(testSendEmail));

        #endregion 
        
        testEnv.AddCommunicationService(communicationServiceMock.Object);
        testEnv.AddRegistrationProducer(producerMock.Object);
        var env = testEnv.Build();

        try
        {
            await env.Harness.Start();
            await env.RegistrationService.RegisterAsync(new RegisterUserDto("", ""));
            var anyCorrectMessagesConsumed =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(env.EmailConsumer,
                    message => message.Context.Message == testSendEmail);
            var anyCorrectMessagesConsumedWithErrors =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(env.EmailConsumer,
                    message => message.Exception is not null);
            
            Assert.True(anyCorrectMessagesConsumed);
            Assert.False(anyCorrectMessagesConsumedWithErrors);
        }
        finally
        {
            await env.Harness.Stop();
        }
    }

    [Homework(RunLogic.Homeworks.MasstransitRabbit)]
    public async Task MailingConsumer_ShouldConsume_Messages_SentFromRegisterService_WithoutErrors()
    {
        await using var testEnv = new TestEnvironmentBuilder();
        testEnv.SetupServices(c => c.AddSingleton<IRegistrationService, RegistrationService>());
        var env = testEnv.Build();
        var consumer = env.EmailConsumer;
    
        try
        {
            await env.Harness.Start();
            await env.RegistrationService.RegisterAsync(new RegisterUserDto("", ""));
            var anyConsumed = await AnyConsumedMessagesWithFilterAsync<SendEmail>(consumer);
            var anyConsumedWithErrors =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(consumer, message => message.Exception is not null);

            Assert.True(anyConsumed);
            Assert.False(anyConsumedWithErrors);
        }
        finally
        {
            await env.Harness.Stop();
        }
    }

    [Homework(RunLogic.Homeworks.MasstransitRabbit)]
    public async Task MailingConsumer_ShouldConsume_OneMessage_WhenOneMessageSentFromRegisterService_WithoutErrors()
    {
        await using var testEnv = new TestEnvironmentBuilder();
        testEnv.SetupServices(c => c.AddSingleton<IRegistrationService, RegistrationService>());
        var env = testEnv.Build();
    
        try
        {
            await env.Harness.Start();
            await env.RegistrationService.RegisterAsync(new RegisterUserDto("", ""));
            var anyConsumed = await AnyConsumedMessagesWithFilterAsync<SendEmail>(env.EmailConsumer);
            var anyConsumedWithErrors =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(env.EmailConsumer, message => message.Exception is not null);
            var countConsumedMessages = CountConsumedMessages<SendEmail>(env.EmailConsumer);
    
            Assert.True(anyConsumed);
            Assert.Equal(1, countConsumedMessages);
            Assert.False(anyConsumedWithErrors);
        }
        finally
        {
            await env.Harness.Stop();
        }
    }
    
    [Homework(RunLogic.Homeworks.MasstransitRabbit)]
    public async Task MailingConsumer_ShouldCall_IMailingService_Once()
    {
        await using var testEnv = new TestEnvironmentBuilder();
        testEnv.SetupServices();

        #region Mocking ICommunicationService and IRegistrationService
        
        var communicationServiceMock = new Mock<ICommunicationService>();
        var testSendEmail = new SendEmail("test", "test", "test", "test");
        var producerMock = new Mock<IRegistrationService>();
        
        communicationServiceMock.Setup(c => c.SendEmailAsync(It.Is<SendEmail>(data => data == testSendEmail)))
            .Callback(async () => await testEnv.Harness!.Bus.Publish(testSendEmail));
        producerMock.Setup(p => p.RegisterAsync(It.IsAny<RegisterUserDto>()))
            .Callback(async () => await communicationServiceMock.Object.SendEmailAsync(testSendEmail));

        #endregion
        
        testEnv.AddRegistrationProducer(producerMock.Object);
        testEnv.AddCommunicationService(communicationServiceMock.Object);
        var env = testEnv.Build();
        try
        {
            await env.Harness.Start();
            await env.RegistrationService.RegisterAsync(new RegisterUserDto("", ""));
            await Task.Delay(100);
    
            env.MailingServiceMock.Verify(m => m.SendEmailAsync(It.IsAny<EmailMessage>()), Times.Once);
        }
        finally
        {
            await env.Harness.Stop();
        }
    }
}