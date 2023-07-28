using Dotnet.Homeworks.Tests.RunLogic.Attributes;

namespace Dotnet.Homeworks.Tests.Cqrs;

public class VerticalSlicesTests
{
    private const string feauturesProjectName = "Dotnet.Homeworks.Features";
    private const string productDirName = "Products";
    private const string queryDirName = "Queries";
    private const string commandDirName = "Commands";
    private readonly DirectoryInfo? projectDir;
    
    public VerticalSlicesTests()
    {
        projectDir = Directory.GetParent(feauturesProjectName)?.Parent?.Parent?.Parent?.Parent;
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public void FeaturesNamespace_ShouldHave_FeaturesDirectory()
    {
        var feautures = projectDir?.GetDirectories()
            .FirstOrDefault(dir => dir.Name == feauturesProjectName);
        var dirExists = feautures?.GetDirectories()?.FirstOrDefault(x=>x.Name==productDirName)?.Exists;

        Assert.True(dirExists);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public void ProductDirectory_ShouldHave_CqrsSlices()
    {
        var list = new List<string>() { queryDirName, commandDirName };
        
        var productsDir = projectDir?
            .GetDirectories()?
            .FirstOrDefault(dir => dir.Name == feauturesProjectName)?
            .GetDirectories()?
            .FirstOrDefault(x => x.Name == productDirName);

        var isCqrsDirsExist = productsDir?
            .GetDirectories()?
            .Select(x => x.Name)?
            .Intersect(list)?
            .Any();

        Assert.True(isCqrsDirsExist);
    }

}