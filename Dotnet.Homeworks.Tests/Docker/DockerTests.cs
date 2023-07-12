using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Dotnet.Homeworks.Tests.Docker;

public class DockerTests
{
    private const string FilePath = "docker-compose.yml";

    private static readonly Lazy<DockerCompose> DockerComposeDeserializedFactory = new(() =>
    {
        var filePath = Path.Combine(TryGetSolutionDirectoryInfo().FullName, FilePath);

        if (!File.Exists(filePath)) throw new InvalidOperationException();

        var b = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .WithNamingConvention(UnderscoredNamingConvention.Instance);

        return b.Build().Deserialize<DockerCompose>(File.ReadAllText(filePath));
    });

    private static readonly DockerCompose DockerComposeDeserialized = DockerComposeDeserializedFactory.Value;

    [Homework(RunLogic.Homeworks.Docker)]
    public void ConnectionStringsDefault_IsNotNullInDockerCompose()
    {
        var dotnetWebConnectionString =
            DockerComposeDeserialized.Services?.DotnetWeb?.Environment?.GetValueOrDefault("ConnectionStrings__Default");

        Assert.NotNull(dotnetWebConnectionString);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetPostgres_ShouldContainUsernameEnvVar()
    {
        var postgresUsernameValue =
            DockerComposeDeserialized.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_USER");

        Assert.NotNull(postgresUsernameValue);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetPostgres_ShouldContainPasswordEnvVar()
    {
        var postgresPasswordValue =
            DockerComposeDeserialized.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_PASSWORD");

        Assert.NotNull(postgresPasswordValue);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetPostgres_ShouldContainDbNameEnvVar()
    {
        var postgresDbNameValue =
            DockerComposeDeserialized.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_DB");

        Assert.NotNull(postgresDbNameValue);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetWeb_ShouldDependOnDotnetPostgres()
    {
        var dotnetPostgresDependencyExists =
            DockerComposeDeserialized.Services?.DotnetWeb?.DependsOn?.Contains("dotnet_postgres");

        Assert.True(dotnetPostgresDependencyExists);
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetWeb_DbConnectionString_ShouldContain_AllDotnetPostgres_EnvVars()
    {
        var dotnetWebConnectionString =
            DockerComposeDeserialized.Services?.DotnetWeb?.Environment?.GetValueOrDefault("ConnectionStrings__Default");
        var postgresUsernameValue =
            DockerComposeDeserialized.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_USER");
        var postgresPasswordValue =
            DockerComposeDeserialized.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_PASSWORD");
        var postgresDbNameValue =
            DockerComposeDeserialized.Services?.DotnetPostgres?.Environment?.GetValueOrDefault("POSTGRES_DB");
       
        Assert.True(dotnetWebConnectionString.Contains(postgresUsernameValue)
                    && dotnetWebConnectionString.Contains(postgresPasswordValue)
                    && dotnetWebConnectionString.Contains(postgresDbNameValue));
    }

    [Homework(RunLogic.Homeworks.Docker)]
    public void DotnetWeb_ShouldContain_CorrectPathToDockerFile()
    {
        var expectedPath = "Dotnet.Homeworks.MainProject/Dockerfile";
        var actualPath = DockerComposeDeserialized.Services?.DotnetWeb?.Build?.GetValueOrDefault("dockerfile");
        
        Assert.Equal(expectedPath, actualPath);
    }

    private static DirectoryInfo TryGetSolutionDirectoryInfo()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        
        while (directory != null && !directory.GetFiles("*.sln").Any()) 
            directory = directory.Parent;
        
        return directory;
    }
}