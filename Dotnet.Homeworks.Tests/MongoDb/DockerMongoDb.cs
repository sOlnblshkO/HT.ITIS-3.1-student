using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.Shared.Docker;

namespace Dotnet.Homeworks.Tests.MongoDb;

public class DockerMongodb
{
    [Homework(RunLogic.Homeworks.MongoDb)]
    public void Services_ShouldContain_Mongodb()
    {
        var docker = Parser.Parse();

        Assert.NotNull(docker.Services?.DotnetMongodb);
    }
    
    [Homework(RunLogic.Homeworks.MongoDb)]
    public void DotnetMongodb_ShouldContain_CredentialEnvVars()
    {
        var docker = Parser.Parse();
        var mongoRootUser =
            docker.Services?.DotnetMongodb?.Environment?.GetValueOrDefault(Constants.MongodbRootUsernameEnvVar);
        var mongoRootPassword =
            docker.Services?.DotnetMongodb?.Environment?.GetValueOrDefault(Constants.MongodbRootPasswordEnvVar);
    
        Assert.NotNull(mongoRootUser);
        Assert.NotNull(mongoRootPassword);
    }
}