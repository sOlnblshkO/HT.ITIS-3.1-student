namespace Dotnet.Homeworks.Tests.Shared.Docker;

public abstract class HasEnvironment
{
    public Dictionary<string, string>? Environment { get; set; }
}