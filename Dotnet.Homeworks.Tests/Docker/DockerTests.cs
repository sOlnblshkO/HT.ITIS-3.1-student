using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.Shared.Docker;

namespace Dotnet.Homeworks.Tests.Docker;

public class DockerTests
{
    [Homework(RunLogic.Homeworks.Docker)]
    public void ConnectionStringsDefault_IsNotNullInDockerCompose()
    {
        var docker = Parser.Parse();
        var dotnetMainConnectionString =
            docker.Services?.DotnetMain?.Environment?.GetValueOrDefault(Constants.MainDefaultConnectionStringEnvVar);

        Assert.NotNull(dotnetMainConnectionString);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetPostgres_ShouldContainUsernameEnvVar()
    {
        var docker = Parser.Parse();
        var postgresUsernameValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault(Constants.PostgresUserEnvVar);

        Assert.NotNull(postgresUsernameValue);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetPostgres_ShouldContainPasswordEnvVar()
    {
        var docker = Parser.Parse();
        var postgresPasswordValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault(Constants.PostgresPasswordEnvVar);

        Assert.NotNull(postgresPasswordValue);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetPostgres_ShouldContainDbNameEnvVar()
    {
        var docker = Parser.Parse();
        var postgresDbNameValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault(Constants.PostgresDbEnvVar);

        Assert.NotNull(postgresDbNameValue);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetMain_ShouldDependOnDotnetPostgres()
    {
        var docker = Parser.Parse();
        var dotnetPostgresDependencyExists =
            docker.Services?.DotnetMain?.DependsOn?.Contains(Constants.PostgresService);

        Assert.True(dotnetPostgresDependencyExists);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetMain_DbConnectionString_ShouldContain_AllDotnetPostgres_EnvVars()
    {
        var docker = Parser.Parse();
        var dotnetMainConnectionString =
            docker.Services?.DotnetMain?.Environment?.GetValueOrDefault(Constants.MainDefaultConnectionStringEnvVar);
        var postgresUsernameValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault(Constants.PostgresUserEnvVar);
        var postgresPasswordValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault(Constants.PostgresPasswordEnvVar);
        var postgresDbNameValue =
            docker.Services?.DotnetPostgres?.Environment?.GetValueOrDefault(Constants.PostgresDbEnvVar);

        Assert.NotNull(dotnetMainConnectionString);
        Assert.NotNull(postgresUsernameValue);
        Assert.NotNull(postgresPasswordValue);
        Assert.NotNull(postgresDbNameValue);
        Assert.True(dotnetMainConnectionString.Contains(postgresUsernameValue)
                    && dotnetMainConnectionString.Contains(postgresPasswordValue)
                    && dotnetMainConnectionString.Contains(postgresDbNameValue));
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetMain_ShouldContain_CorrectPathToDockerFile()
    {
        var docker = Parser.Parse();
        var expectedPath = "Dotnet.Homeworks.MainProject/Dockerfile";
        var actualPath = docker.Services?.DotnetMain?.Build?.GetValueOrDefault("dockerfile");

        Assert.Equal(expectedPath, actualPath);
    }
}