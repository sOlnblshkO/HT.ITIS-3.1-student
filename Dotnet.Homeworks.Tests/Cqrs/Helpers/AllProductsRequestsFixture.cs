using System.Reflection;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

[CollectionDefinition(nameof(AllProductsRequestsFixture))]
public class AllProductsRequestsFixture : IDisposable, ICollectionFixture<AllProductsRequestsFixture>
{
    private static Assembly AssemblyFeatures = Features.Helpers.AssemblyReference.Assembly;

    public AllProductsRequestsFixture()
    {
        if (!AllRequestsInAssemblyFixture() || !AllHandlersInAssemblyFixture())
            throw new ProductImplementInterfacesException(AssemblyFeatures.FullName ?? "");
    }

    public bool AllRequestsInAssemblyFixture()
    {
        var interfaces = new List<Type>()
        {
            typeof(ICommand<>),
            typeof(ICommand),
            typeof(IQuery<>)
        };

        var types = AssemblyFeatures.GetTypes()
            .Where(x => x.Namespace.Contains("Products"))
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
            .Where(x => x.Namespace.Contains("Products"))
            .Where(x => x.Name.EndsWith("Handler"))
            .Select(x => interfaces.IntersectBy(x.GetInterfaces().Select(x => x.Name), type => type.Name));

        return types.All(x => x.Any());
    }

    public void Dispose() =>
        GC.SuppressFinalize(this);
}