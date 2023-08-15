namespace Dotnet.Homeworks.Tests.RunLogic.Utils.Cqrs;

public static class Constants
{
    public const string MediatR = "MediatR";
    public static string? MainProjectNamespace = typeof(MainProject.Helpers.AssemblyReference).Assembly.GetName().Name;
    public static string? CustomMediatorNamespace = typeof(Mediator.Helpers.AssemblyReference).Assembly.GetName().Name;
    public const string UsersFeatureNamespace = "Dotnet.Homeworks.Features.Cqrs.Users";
    public const string UserManagementFeatureNamespace = "Dotnet.Homeworks.Features.Cqrs.UserManagement";
}