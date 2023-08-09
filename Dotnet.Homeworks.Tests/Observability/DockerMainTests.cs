using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

namespace Dotnet.Homeworks.Tests.Observability;

public class DockerMainTests
{
    [Homework(RunLogic.Homeworks.Observability)]
    public void DotnetMain_ShouldDependOn_DotnetJaeger()
    {
        var docker = Parser.Parse();
        var dotnetJaegerDependencyExists =
            docker.Services?.DotnetMain?.DependsOn?.Contains(Constants.JaegerService);

        Assert.True(dotnetJaegerDependencyExists);
    }
}