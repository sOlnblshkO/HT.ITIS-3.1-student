using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

namespace Dotnet.Homeworks.Tests.Masstransit;

public class DockerMailingTests
{
    [Homework(RunLogic.Homeworks.Rabbit)]
    public void DotnetMailing_ShouldContain_CorrectPathToDockerFile()
    {
        var docker = Parser.Parse();
        var expectedPath = "Dotnet.Homeworks.Mailing.API/Dockerfile";
        var actualPath = docker.Services?.DotnetMailing?.Build?.GetValueOrDefault("dockerfile");
        
        Assert.Equal(expectedPath, actualPath);
    }
    
    [Homework(RunLogic.Homeworks.Rabbit)]
    public void DotnetMailing_ShouldDependOn_DotnetRabbitMq()
    {
        var docker = Parser.Parse();
        var dotnetRabbitmqDependencyExists =
            docker.Services?.DotnetMailing?.DependsOn?.Contains(Defaults.RabbitMqService);

        Assert.True(dotnetRabbitmqDependencyExists);
    }
    
    [Homework(RunLogic.Homeworks.Rabbit)]
    public void DotnetMailing_ShouldNotDependOn_DotnetPostgres()
    {
        var docker = Parser.Parse();
        var dotnetRabbitmqDependencyExists =
            docker.Services?.DotnetMailing?.DependsOn?.Contains(Defaults.PostgresService);

        Assert.False(dotnetRabbitmqDependencyExists);
    }
    
    [Homework(RunLogic.Homeworks.Rabbit)]
    public void DotnetMailing_ShouldContain_EmailConfigEnvVars()
    {
        var docker = Parser.Parse();
        var requiredVars = new HashSet<string>
        {
            Defaults.MailingEmailConfig.Email, Defaults.MailingEmailConfig.Host,
            Defaults.MailingEmailConfig.Port, Defaults.MailingEmailConfig.Password
        };

        // violates AAA a bit but ok
        Assert.NotNull(docker.Services?.DotnetMailing?.Environment);
        
        foreach (var key in docker.Services?.DotnetMailing?.Environment?.Keys!)
        {
            var item = key.Split("__")[^1];
            if (requiredVars.Contains(item))
                requiredVars.Remove(item);
        }

        Assert.Empty(requiredVars);
    }
}