using System.Reflection;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

[CollectionDefinition(nameof(AllProductsRequestsFixture))]
public class AllProductsRequestsFixture : IDisposable, ICollectionFixture<AllProductsRequestsFixture>
{
    private static readonly Assembly AssemblyFeatures = Features.Helpers.AssemblyReference.Assembly;

    public AllProductsRequestsFixture()
    {
        if (!AllRequestsInAssemblyFixture() || !AllHandlersInAssemblyFixture())
            throw new ImplementInterfacesException(
                $"Not all Products feature types implement required interfaces in {AssemblyFeatures.GetName().FullName} assembly"
            );
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
            .Where(type => type.Namespace != null && type.Namespace.Contains("Products"))
            .Where(type => type.Name.EndsWith("Command") || type.Name.EndsWith("Query"))
            .Select(type => interfaces.IntersectBy(type.GetInterfaces().Select(interfaceType => interfaceType.Name), x => x.Name));

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
            .Where(type => type.Namespace != null && type.Namespace.Contains("Products"))
            .Where(type => type.Name.EndsWith("Handler"))
            .Select(type => interfaces.IntersectBy(type.GetInterfaces().Select(interfaceType => interfaceType.Name), x => x.Name));

        return types.All(x => x.Any());
    }

    public void Dispose() =>
        GC.SuppressFinalize(this);
}