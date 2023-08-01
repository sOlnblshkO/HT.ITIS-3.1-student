using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

namespace Dotnet.Homeworks.Tests.MinioStorage;

public class DockerMinioTests
{
    [Homework(RunLogic.Homeworks.MinioStorage)]
    public void Services_ShouldContain_DotnetMinio()
    {
        var docker = Parser.Parse();

        Assert.NotNull(docker.Services?.DotnetMinio);
    }
    
    [Homework(RunLogic.Homeworks.MinioStorage)]
    public void DotnetMinio_ShouldContain_CredentialEnvVars()
    {
        var docker = Parser.Parse();
        var minioDefaultUser =
            docker.Services?.DotnetMinio?.Environment?.GetValueOrDefault(Constants.MinioRootUserEnvVar);
        var minioDefaultPassword =
            docker.Services?.DotnetMinio?.Environment?.GetValueOrDefault(Constants.MinioRootPassEnvVar);

        Assert.NotNull(minioDefaultUser);
        Assert.NotNull(minioDefaultPassword);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public void DotnetMinio_ShouldContain_CredentialEnvVars_OtherThanMinioAdmin()
    {
        var docker = Parser.Parse();
        var minioDefaultUser =
            docker.Services?.DotnetMinio?.Environment?.GetValueOrDefault(Constants.MinioRootUserEnvVar);
        var minioDefaultPassword =
            docker.Services?.DotnetMinio?.Environment?.GetValueOrDefault(Constants.MinioRootPassEnvVar);

        Assert.NotNull(minioDefaultUser);
        Assert.NotNull(minioDefaultPassword);
        Assert.NotEqual("minioadmin", minioDefaultUser);
        Assert.NotEqual("minioadmin", minioDefaultPassword);
    }
}