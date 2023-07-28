using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

namespace Dotnet.Homeworks.Tests.MinioStorage;

public class DockerStorageTests
{
    [Homework(RunLogic.Homeworks.MinioStorage)]
    public void DotnetStorage_ShouldContain_CorrectPathToDockerFile()
    {
        var docker = Parser.Parse();
        var expectedPath = "Dotnet.Homeworks.Storage.API/Dockerfile";
        var actualPath = docker.Services?.DotnetStorage?.Build?.GetValueOrDefault("dockerfile");
        
        Assert.Equal(expectedPath, actualPath);
    }
    
    [Homework(RunLogic.Homeworks.MinioStorage)]
    public void DotnetStorage_ShouldDependOn_DotnetMinio()
    {
        var docker = Parser.Parse();
        var dotnetMinioDependencyExists =
            docker.Services?.DotnetStorage?.DependsOn?.Contains(Constants.MinioService);

        Assert.True(dotnetMinioDependencyExists);
    }
    
    [Homework(RunLogic.Homeworks.MinioStorage)]
    public void DotnetStorage_ShouldDependOn_OnlyDotnetMinio()
    {
        var docker = Parser.Parse();
        var dotnetStorageDependencies = docker.Services?.DotnetStorage?.DependsOn;

        Assert.Equal(1, dotnetStorageDependencies?.Count);
        Assert.Equal(Constants.MinioService, dotnetStorageDependencies?[0]);
    }
    
    [Homework(RunLogic.Homeworks.MinioStorage)]
    public void DotnetStorage_ShouldContain_MinioConfigEnvVars()
    {
        var docker = Parser.Parse();
        var requiredVars = new HashSet<string>
        {
            Constants.StorageMinioConfig.Username, Constants.StorageMinioConfig.Password,
            Constants.StorageMinioConfig.Endpoint, Constants.StorageMinioConfig.Port,
            Constants.StorageMinioConfig.WithSsl
        };

        Assert.NotNull(docker.Services?.DotnetStorage?.Environment);
        
        foreach (var key in docker.Services?.DotnetStorage?.Environment?.Keys!)
        {
            var item = key.Split("__")[^1];
            if (requiredVars.Contains(item))
                requiredVars.Remove(item);
        }

        Assert.Empty(requiredVars);
    }
}