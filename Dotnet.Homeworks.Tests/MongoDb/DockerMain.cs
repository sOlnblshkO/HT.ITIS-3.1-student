using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.Shared.Docker;
using static Dotnet.Homeworks.Tests.Shared.Docker.Constants;

namespace Dotnet.Homeworks.Tests.MongoDb;

public class DockerMain
{
    [Homework(RunLogic.Homeworks.MongoDb)]
    public void DotnetMain_ShouldDependOn_DotnetMongo()
    {
        var docker = Parser.Parse();
        var dotnetMongoDependencyExists =
            docker.Services?.DotnetMain?.DependsOn?.Contains(MongodbService);

        Assert.True(dotnetMongoDependencyExists);
    }
}