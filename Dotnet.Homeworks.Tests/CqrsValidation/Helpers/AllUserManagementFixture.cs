using System.Reflection;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;

namespace Dotnet.Homeworks.Tests.CqrsValidation.Helpers;

[CollectionDefinition(nameof(AllUserManagementFixture))]
public class AllUserManagementFixture : IDisposable, ICollectionFixture<AllUserManagementFixture>
{
    private static Assembly AssemblyFeatures = Features.Helpers.AssemblyReference.Assembly;

    public AllUserManagementFixture()
    {
        if (!AllRequestsInAssemblyFixture() || !AllHandlersInAssemblyFixture())
            throw new UserImplementInterfacesException(AssemblyFeatures.GetName().FullName);
    }

    private bool AllRequestsInAssemblyFixture()
    {
        var interfaces = new List<Type>()
        {
            typeof(ICommand<>),
            typeof(ICommand),
            typeof(IQuery<>)
        };

        var types = AssemblyFeatures.GetTypes()
            .Where(x => x.Namespace.Contains("UserManagement"))
            .Where(x => x.Name.EndsWith("Command") || x.Name.EndsWith("Query"))
            .Select(x => interfaces.IntersectBy(x.GetInterfaces().Select(x => x.Name), type => type.Name));

        return types.All(x => x.Any());
    }

    public bool AllHandlersInAssemblyFixture()
    {
        var interfaces = new List<Type>()
        {
            typeof(ICommandHandler<,>),
            typeof(ICommandHandler<>),
            typeof(IQueryHandler<,>),
        };

        var types = AssemblyFeatures.GetTypes()
            .Where(x => x.Namespace.Contains("UserManagement"))
            .Where(x => x.Name.EndsWith("Handler"))
            .Select(x => interfaces.IntersectBy(x.GetInterfaces().Select(x => x.Name), type => type.Name));

        return types.All(x => x.Any());
    }

    public void Dispose() =>
        GC.SuppressFinalize(this);
}