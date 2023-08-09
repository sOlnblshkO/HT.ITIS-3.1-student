using Dotnet.Homeworks.MainProject.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Observability;
using NetArchTest.Rules;

namespace Dotnet.Homeworks.Tests.Observability;

public class ArchitectureTests
{
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public void MainProject_ShouldHave_DependencyOn_OpenTelemetry()
    {
        var mainAssembly = AssemblyReference.Assembly;

        var types = Types
            .InAssembly(mainAssembly)
            .That()
            .HaveDependencyOn(Constants.OpenTelemetry)
            .GetTypes();
        
        Assert.NotEmpty(types);
    }
    
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public void MainProject_ShouldHave_DependencyOn_OpenTelemetryTrace()
    {
        var mainAssembly = AssemblyReference.Assembly;

        var types = Types
            .InAssembly(mainAssembly)
            .That()
            .HaveDependencyOn(Constants.OpenTelemetryTrace)
            .GetTypes();
        
        Assert.NotEmpty(types);
    }
    
    [Homework(RunLogic.Homeworks.RabbitMasstransit)]
    public void MainProject_ShouldHave_DependencyOn_OpenTelemetryMetrics()
    {
        var mainAssembly = AssemblyReference.Assembly;

        var types = Types
            .InAssembly(mainAssembly)
            .That()
            .HaveDependencyOn(Constants.OpenTelemetryMetrics)
            .GetTypes();
        
        Assert.NotEmpty(types);
    }
}