using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit;

public class MasstransitProducersTests
{
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public async Task RegisterService_ShouldPublishOrSend_SendEmail_WithoutErrors()
    {
        await using var testEnvBuilder = new MasstransitEnvironmentBuilder();
        testEnvBuilder.SetupServices(c => c.AddSingleton<IRegistrationService, RegistrationService>());
        var env = testEnvBuilder.Build();

        try
        {
            await env.Harness.Start();
            await env.RegistrationService.RegisterAsync(new RegisterUserDto("", ""), new CancellationToken());

            Assert.True(await env.Harness.Published.Any<SendEmail>() || await env.Harness.Sent.Any<SendEmail>());
            Assert.False(await env.Harness.Published.Any<SendEmail>(p => p.Exception is not null) ||
                         await env.Harness.Sent.Any<SendEmail>(p => p.Exception is not null));
        }
        finally
        {
            await env.Harness.Stop();
        }
    }
}