using Dotnet.Homeworks.Features.Cqrs.UserManagement;

namespace Dotnet.Homeworks.Tests.RunLogic.Utils.Cqrs;

public static class Constants
{
    public const string MediatR = "MediatR";
    public static string? MainProjectNamespace = MainProject.Helpers.AssemblyReference.Assembly.GetName().Name;
    public static string? CustomMediatorNamespace = Mediator.Helpers.AssemblyReference.Assembly.GetName().Name;
    public static string UsersFeatureNamespace = Features.Cqrs.Users.DirectoryReference.Namespace;
    public static string UserManagementFeatureNamespace = DirectoryReference.Namespace;
}