using Dotnet.Homeworks.Mailing.API.Dto;
using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using static Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers.ReflectionHelpers;
// ReSharper disable AccessToDisposedClosure

namespace Dotnet.Homeworks.Tests.MasstransitRabbit;

[Collection(nameof(AnyConsumersInAssemblyFixture))]
public class MasstransitConsumerTests
{
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public async Task MailingConsumer_ShouldConsume_CorrectMessages_WithoutErrors()
    {
        await using var testEnvBuilder = new TestEnvironmentBuilder();
        var testSendEmail = new SendEmail("test", "test", "test", "test");
        testEnvBuilder.SetupServices();
        testEnvBuilder.SetupProducingProcessMock(testSendEmail);
        var env = testEnvBuilder.Build();

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

    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public async Task MailingConsumer_ShouldConsume_Messages_SentFromRegisterService_WithoutErrors()
    {
        await using var testEnvBuilder = new TestEnvironmentBuilder();
        testEnvBuilder.SetupServices(c => c.AddSingleton<IRegistrationService, RegistrationService>());
        var env = testEnvBuilder.Build();
    
        try
        {
            await env.Harness.Start();
            await env.RegistrationService.RegisterAsync(new RegisterUserDto("", ""));
            var anyConsumed = await AnyConsumedMessagesWithFilterAsync<SendEmail>(env.EmailConsumer);
            var anyConsumedWithErrors =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(env.EmailConsumer,
                    message => message.Exception is not null);

            Assert.True(anyConsumed);
            Assert.False(anyConsumedWithErrors);
        }
        finally
        {
            await env.Harness.Stop();
        }
    }

    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public async Task MailingConsumer_ShouldConsume_OneMessage_WhenOneMessageSentFromRegisterService_WithoutErrors()
    {
        await using var testEnvBuilder = new TestEnvironmentBuilder();
        testEnvBuilder.SetupServices(c => c.AddSingleton<IRegistrationService, RegistrationService>());
        var env = testEnvBuilder.Build();
    
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
    
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public async Task MailingConsumer_ShouldCall_IMailingService_Once()
    {
        await using var testEnvBuilder = new TestEnvironmentBuilder();
        var testSendEmail = new SendEmail("test", "test", "test", "test");
        testEnvBuilder.SetupServices();
        testEnvBuilder.SetupProducingProcessMock(testSendEmail);
        var env = testEnvBuilder.Build();

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