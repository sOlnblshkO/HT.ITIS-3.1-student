using Dotnet.Homeworks.Features.Users;

namespace Dotnet.Homeworks.Tests.RunLogic.Utils.Cqrs;

public static class Constants
{
    public const string MediatR = nameof(MediatR);
    public static readonly string CustomMediatorNamespace = Mediator.Helpers.AssemblyReference.Assembly.GetName().Name!;
    public static readonly string UsersFeatureNamespace = DirectoryReference.Namespace!;
    public static readonly string UserManagementFeatureNamespace = Features.UserManagement.DirectoryReference.Namespace!;
}