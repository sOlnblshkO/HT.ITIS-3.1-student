using Dotnet.Homeworks.Features.Cqrs.UserManagement;

namespace Dotnet.Homeworks.Tests.RunLogic.Utils.Cqrs;

public static class Constants
{
    public const string MediatR = nameof(MediatR);
    public static readonly string CustomMediatorNamespace = Mediator.Helpers.AssemblyReference.Assembly.GetName().Name!;
    public static readonly string UsersFeatureNamespace = Features.Cqrs.Users.DirectoryReference.Namespace!;
    public static readonly string UserManagementFeatureNamespace = DirectoryReference.Namespace!;
}