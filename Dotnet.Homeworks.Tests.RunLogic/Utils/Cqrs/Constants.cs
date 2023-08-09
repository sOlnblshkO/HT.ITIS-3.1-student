namespace Dotnet.Homeworks.Tests.RunLogic.Utils.Cqrs;

public static class Constants
{
    public const string MediatR = "MediatR";
    public static string? NamespaceMainProject = typeof(MainProject.Helpers.AssemblyReference).Assembly.GetName().Name;
}