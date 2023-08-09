using System.Reflection;
using Dotnet.Homeworks.MainProject.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Observability;
using NetArchTest.Rules;

namespace Dotnet.Homeworks.Tests.Observability;

public class ArchitectureTests
{
    private readonly Assembly _mainAssembly = AssemblyReference.Assembly;

    [HomeworkTheory(RunLogic.Homeworks.RabbitMasstransit)]
    [InlineData(Constants.OpenTelemetry)]
    [InlineData(Constants.OpenTelemetryTrace)]
    [InlineData(Constants.OpenTelemetryMetrics)]
    public void MainProject_ShouldHave_Dependencies(string dependency)
    {
        var types = GetTypesInAssemblyThatHaveDependency(_mainAssembly, dependency);

        Assert.NotEmpty(types);
    }

    private static IEnumerable<Type> GetTypesInAssemblyThatHaveDependency(Assembly assembly, string dependency)
    {
        return Types
            .InAssembly(assembly)
            .That()
            .HaveDependencyOn(dependency)
            .GetTypes();
    }
}