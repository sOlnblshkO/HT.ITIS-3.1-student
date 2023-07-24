using Dotnet.Homeworks.Mailing.API.Consumers;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using NetArchTest.Rules;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit;

public class ArchitectureTests
{
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public void ConsumersNamespace_ShouldHave_DependencyOnMasstransit()
    {
        var consumerType = typeof(IEmailConsumer);
        
        var testResult = Types
            .InNamespace(consumerType.Namespace)
            .Should()
            .HaveDependencyOn("MassTransit")
            .GetResult();
        
        Assert.True(testResult.IsSuccessful);
    }

    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public void MainProjectAssembly_ShouldNotHave_DependencyOnMailingAPI()
    {
        var mainAssembly = typeof(RegistrationService).Assembly;
        var mailingFullName = typeof(EmailConsumer).FullName;

        var testResult = Types.InAssembly(mainAssembly)
            .ShouldNot()
            .HaveDependencyOn(mailingFullName)
            .GetResult();
        
        Assert.True(testResult.IsSuccessful);
    }

    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public void MailingAPIAssembly_ShouldNotHave_DependencyOnMainProject()
    {
        var mailingAssembly = typeof(EmailConsumer).Assembly;
        var mainFullName = typeof(RegistrationService).FullName;

        var testResult = Types.InAssembly(mailingAssembly)
            .ShouldNot()
            .HaveDependencyOn(mainFullName)
            .GetResult();
        
        Assert.True(testResult.IsSuccessful);
    }
}