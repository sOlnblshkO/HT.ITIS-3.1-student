using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

namespace Dotnet.Homeworks.Tests.Docker;

public class DockerTests
{
    [Homework(RunLogic.Homeworks.Docker)]
    public void ConnectionStringsDefault_IsNotNullInDockerCompose()
    {
        var docker = Parser.Parse();
        var dotnetWebConnectionString =
            docker.Services?.DotnetWeb?.Environment?.GetValueOrDefault("ConnectionStrings__Default");

        Assert.NotNull(dotnetWebConnectionString);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetPostgres_ShouldContainUsernameEnvVar()
    {
        var docker = Parser.Parse();
        var postgresUsernameValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_USER");

        Assert.NotNull(postgresUsernameValue);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetPostgres_ShouldContainPasswordEnvVar()
    {
        var docker = Parser.Parse();
        var postgresPasswordValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_PASSWORD");

        Assert.NotNull(postgresPasswordValue);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetPostgres_ShouldContainDbNameEnvVar()
    {
        var docker = Parser.Parse();
        var postgresDbNameValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_DB");

        Assert.NotNull(postgresDbNameValue);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetWeb_ShouldDependOnDotnetPostgres()
    {
        var docker = Parser.Parse();
        var dotnetPostgresDependencyExists =
            docker.Services?.DotnetWeb?.DependsOn?.Contains("dotnet_postgres");

        Assert.True(dotnetPostgresDependencyExists);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetWeb_DbConnectionString_ShouldContain_AllDotnetPostgres_EnvVars()
    {
        var docker = Parser.Parse();
        var dotnetWebConnectionString =
            docker.Services?.DotnetWeb?.Environment?.GetValueOrDefault("ConnectionStrings__Default");
        var postgresUsernameValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_USER");
        var postgresPasswordValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_PASSWORD");
        var postgresDbNameValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_DB");

        Assert.NotNull(dotnetWebConnectionString);
        Assert.NotNull(postgresUsernameValue);
        Assert.NotNull(postgresPasswordValue);
        Assert.NotNull(postgresDbNameValue);
        Assert.True(dotnetWebConnectionString.Contains(postgresUsernameValue)
                    && dotnetWebConnectionString.Contains(postgresPasswordValue)
                    && dotnetWebConnectionString.Contains(postgresDbNameValue));
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetWeb_ShouldContain_CorrectPathToDockerFile()
    {
        const string expectedPath = "Dotnet.Homeworks.MainProject/Dockerfile";
        var docker = Parser.Parse();
        var actualPath = docker.Services?.DotnetWeb?.Build?.GetValueOrDefault("dockerfile");
        
        Assert.Equal(expectedPath, actualPath);
    }
}