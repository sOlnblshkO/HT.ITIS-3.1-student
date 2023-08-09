using System.Reflection;
using Dotnet.Homeworks.MainProject.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Observability;
using NetArchTest.Rules;

namespace Dotnet.Homeworks.Tests.Observability;

public class ArchitectureTests
{
    private readonly Assembly _mainAssembly = AssemblyReference.Assembly;
    
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public void MainProject_ShouldHave_DependencyOn_OpenTelemetry()
    {
        var types = GetTypesInAssemblyThatHaveDependency(_mainAssembly, Constants.OpenTelemetry);
        
        Assert.NotEmpty(types);
    }
    
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public void MainProject_ShouldHave_DependencyOn_OpenTelemetryTrace()
    {
        var types = GetTypesInAssemblyThatHaveDependency(_mainAssembly, Constants.OpenTelemetryTrace);
        
        Assert.NotEmpty(types);
    }
    
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public void MainProject_ShouldHave_DependencyOn_OpenTelemetryMetrics()
    {
        var types = GetTypesInAssemblyThatHaveDependency(_mainAssembly, Constants.OpenTelemetryMetrics);
        
        Assert.NotEmpty(types);
    }

    private static IEnumerable<Type> GetTypesInAssemblyThatHaveDependency(Assembly assembly, string dependency) =>
        Types
            .InAssembly(assembly)
            .That()
            .HaveDependencyOn(dependency)
            .GetTypes();
}