using System.Reflection;
using Dotnet.Homeworks.MainProject.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Observability;
using NetArchTest.Rules;

namespace Dotnet.Homeworks.Tests.Observability;

public class ArchitectureTests
{
    private readonly Assembly _mainAssembly = AssemblyReference.Assembly;

    [HomeworkTheory(RunLogic.Homeworks.Observability)]
    [InlineData(Constants.OpenTelemetry)]
    [InlineData(Constants.OpenTelemetryTrace)]
    [InlineData(Constants.OpenTelemetryMetrics)]
    public void MainProject_ShouldHave_Dependency(string dependency)
    {
        var types = Types
            .InAssembly(_mainAssembly)
            .That()
            .HaveDependencyOn(dependency)
            .GetTypes();

        Assert.NotEmpty(types);
    }
}