using System.Reflection;
using Dotnet.Homeworks.DataAccess.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using NetArchTest.Rules;
using static Dotnet.Homeworks.Tests.Shared.MongoDb.Constants;

namespace Dotnet.Homeworks.Tests.MongoDb;

public class ArchitectureTests
{
    private readonly Assembly _orderRepositoryAssembly = AssemblyReference.Assembly;
    
    [Homework(RunLogic.Homeworks.MongoDb)]
    public void OrderRepository_ShouldHave_DependencyOn_MongoDriver()
    {
        var testResult = Types
            .InAssembly(_orderRepositoryAssembly)
            .That()
            .HaveName(OrderRepositoryName)
            .Should()
            .HaveDependencyOn(MongoDriverDependencyName)
            .GetResult();
        
        Assert.True(testResult.IsSuccessful);
    }
}