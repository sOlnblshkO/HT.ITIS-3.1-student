using System.Reflection;
using Dotnet.Homeworks.Storage.API.Helpers;
using Dotnet.Homeworks.Storage.API.Services;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using NetArchTest.Rules;

namespace Dotnet.Homeworks.Tests.MinioStorage;

public class ArchitectureTests
{
    private readonly Assembly _storageAssembly = AssemblyReference.Assembly;

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public void ImageStorage_ShouldHave_DependencyOnMinio()
    {
        var testResult = Types
            .InAssembly(_storageAssembly)
            .That()
            .ImplementInterface(typeof(IStorage<>))
            .Should()
            .HaveDependencyOn("Minio")
            .GetResult();

        Assert.True(testResult.IsSuccessful);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public void StorageFactory_ShouldHave_DependencyOnMinio()
    {
        var testResult = Types
            .InAssembly(_storageAssembly)
            .That()
            .ImplementInterface(typeof(IStorageFactory))
            .Should()
            .HaveDependencyOn("Minio")
            .GetResult();

        Assert.True(testResult.IsSuccessful);
    }

    [Homework(RunLogic.Homeworks.MinioStorage)]
    public void StorageAPI_ShouldNotHave_DependencyOn_MainProject_And_MailingAPI()
    {
        var mainProjectAssembly = MainProject.Helpers.AssemblyReference.Assembly;
        var mailingApiAssembly = Mailing.API.Helpers.AssemblyReference.Assembly;
        var projectsNamespaces = mainProjectAssembly.ExportedTypes.Select(t => t.Namespace).Distinct()
            .Concat(mailingApiAssembly.ExportedTypes.Select(t => t.Namespace).Distinct()).Where(n => n is not null);

        var testResult = Types
            .InAssembly(_storageAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(projectsNamespaces.ToArray())
            .GetResult();

        Assert.True(testResult.IsSuccessful);
    }
}