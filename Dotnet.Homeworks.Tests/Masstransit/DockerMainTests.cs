using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

namespace Dotnet.Homeworks.Tests.Masstransit;

public class DockerMainTests
{
    [Homework(RunLogic.Homeworks.Rabbit)]
    public void DotnetMain_ShouldDependOn_DotnetPostgres_And_RabbitMq()
    {
        var docker = Parser.Parse();
        var dotnetPostgresDependencyExists =
            docker.Services?.DotnetMain?.DependsOn?.Contains(Defaults.PostgresService);
        var dotnetRabbitMqDependencyExists =
            docker.Services?.DotnetMain?.DependsOn?.Contains(Defaults.RabbitMqService);

        Assert.True(dotnetPostgresDependencyExists);
        Assert.True(dotnetRabbitMqDependencyExists);
    }
}