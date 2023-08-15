using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

namespace Dotnet.Homeworks.Tests.MongoDb;

public class DockerMongodb
{
    [Homework(RunLogic.Homeworks.MongoDb)]
    public void Services_ShouldContain_Mongodb()
    {
        var docker = Parser.Parse();

        Assert.NotNull(docker.Services?.DotnetMongodb);
    }
    
    // uncomment if figure out how do configure mongodb with credentials
    // [Homework(RunLogic.Homeworks.MongoDb)]
    // public void DotnetMongodb_ShouldContain_CredentialEnvVars()
    // {
    //     var docker = Parser.Parse();
    //     var mongoRootUser =
    //         docker.Services?.DotnetMongodb?.Environment?.GetValueOrDefault(Constants.MongodbRootUsernameEnvVar);
    //     var mongoRootPassword =
    //         docker.Services?.DotnetMongodb?.Environment?.GetValueOrDefault(Constants.MongodbRootPasswordEnvVar);
    //
    //     Assert.NotNull(mongoRootUser);
    //     Assert.NotNull(mongoRootPassword);
    // }
}