using Dotnet.Homeworks.Mailing.API.Consumers;
using Dotnet.Homeworks.Mailing.API.Helpers;
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
        var mailingApiAssembly = AssemblyReference.Assembly;
        var mainProjectAssembly = MainProject.Helpers.AssemblyReference.Assembly;
        var mailingApiNamespaces = mailingApiAssembly.ExportedTypes.Select(t => t.Namespace).Distinct()
            .Where(n => n is not null);

        var testResult = Types
            .InAssembly(mainProjectAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(mailingApiNamespaces.ToArray())
            .GetResult();

        Assert.True(testResult.IsSuccessful);
    }

    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public void MailingAPIAssembly_ShouldNotHave_DependencyOnMainProject()
    {
        var mailingApiAssembly = AssemblyReference.Assembly;
        var mainProjectAssembly = MainProject.Helpers.AssemblyReference.Assembly;
        var mainProjectNamespaces =
            mainProjectAssembly.ExportedTypes.Select(t => t.Namespace).Distinct().Where(n => n is not null);

        var testResult = Types
            .InAssembly(mailingApiAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(mainProjectNamespaces.ToArray())
            .GetResult();

        Assert.True(testResult.IsSuccessful);
    }
}