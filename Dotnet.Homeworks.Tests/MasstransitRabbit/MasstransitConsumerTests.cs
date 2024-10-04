using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using static Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers.ReflectionHelpers;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit;

[Collection(nameof(AnyConsumersInAssemblyFixture))]
public class MasstransitConsumerTests
{
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public async Task MailingConsumer_ShouldConsume_Messages_SentFromRegisterService_WithoutErrors()
    {
        await using var testEnvBuilder = new MasstransitEnvironmentBuilder();
        testEnvBuilder.SetupServices(c => c.AddSingleton<IRegistrationService, RegistrationService>());
        var env = testEnvBuilder.Build();

        try
        {
            await env.Harness.Start();
            await env.RegistrationService.RegisterAsync(new RegisterUserDto("", ""), new CancellationToken());
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
        await using var testEnvBuilder = new MasstransitEnvironmentBuilder();
        testEnvBuilder.SetupServices(c => c.AddSingleton<IRegistrationService, RegistrationService>());
        var env = testEnvBuilder.Build();

        try
        {
            await env.Harness.Start();
            await env.RegistrationService.RegisterAsync(new RegisterUserDto("", ""), new CancellationToken());
            var anyConsumed = await AnyConsumedMessagesWithFilterAsync<SendEmail>(env.EmailConsumer);
            var anyConsumedWithErrors =
                await AnyConsumedMessagesWithFilterAsync<SendEmail>(env.EmailConsumer,
                    message => message.Exception is not null);
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
}