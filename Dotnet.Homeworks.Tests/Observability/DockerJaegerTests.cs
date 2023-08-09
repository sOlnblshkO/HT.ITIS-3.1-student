using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

namespace Dotnet.Homeworks.Tests.Observability;

public class DockerJaegerTests
{
    [Homework(RunLogic.Homeworks.Observability)]
    public void Services_ShouldContain_DotnetJaeger()
    {
        var docker = Parser.Parse();
        
        Assert.NotNull(docker.Services?.DotnetJaeger);
    }

    [Homework(RunLogic.Homeworks.Observability)]
    public void DotnetJaeger_ShouldContain_OtlpCollectorEnvVar_AssignedToTrue()
    {
        var docker = Parser.Parse();
        var jaegerOtlpCollectorEnabled =
            docker.Services?.DotnetJaeger?.Environment?.GetValueOrDefault(Constants.JaegerOtlpCollectorEnvVar);

        Assert.True(bool.TryParse(jaegerOtlpCollectorEnabled?.ToLower(), out var enabled));
        Assert.True(enabled);
    }
}