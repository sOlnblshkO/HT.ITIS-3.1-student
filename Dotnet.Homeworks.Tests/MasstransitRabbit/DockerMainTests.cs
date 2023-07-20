using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit;

public class DockerMainTests
{
    [Homework(RunLogic.Homeworks.MasstransitRabbit)]
    public void DotnetMain_ShouldDependOn_DotnetPostgres_And_RabbitMq()
    {
        var docker = Parser.Parse();
        var dotnetPostgresDependencyExists =
            docker.Services?.DotnetMain?.DependsOn?.Contains(Constants.PostgresService);
        var dotnetRabbitMqDependencyExists =
            docker.Services?.DotnetMain?.DependsOn?.Contains(Constants.RabbitMqService);

        Assert.True(dotnetPostgresDependencyExists);
        Assert.True(dotnetRabbitMqDependencyExists);
    }
}