using System.Reflection;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;

namespace Dotnet.Homeworks.Tests.CqrsValidation.Helpers;


[CollectionDefinition(nameof(AllUsersRequestsFixture))]
public class AllUsersRequestsFixture : IDisposable, ICollectionFixture<AllUsersRequestsFixture>
{
    private static Assembly AssemblyFeatures = Features.Helpers.AssemblyReference.Assembly;

    public AllUsersRequestsFixture() {
        if (!AllRequestsInAssemblyFixture() || !AllHandlersInAssemblyFixture())
            throw new UserImplementInterfacesException(AssemblyFeatures.FullName);
    }

    public bool AllRequestsInAssemblyFixture()
    {
        var interfaces = new List<Type>()
        {
            typeof(ICommand<>),
            typeof(ICommand),
            typeof(IQuery<>)
        };
        
        // В списке еще есть GetAllUsers, хотя тот находится в другой директории 
        var types2 = AssemblyFeatures.GetTypes()
            .Where(x=>x.Namespace.Contains("Users"))
            .Where(x => x.Name.EndsWith("Command") || x.Name.EndsWith("Query"));
        
        var types = types2
            .Select(x => interfaces.IntersectBy(x.GetInterfaces().Select(x=>x.Name), type=>type.Name));

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
            .Where(x=>x.Namespace.Contains("Users"))
            .Where(x => x.Name.EndsWith("Handler"))
            .Select(x => interfaces.IntersectBy(x.GetInterfaces().Select(x=>x.Name), type=>type.Name));

        return types.All(x => x.Any());
    }
    
    public void Dispose()=>
        GC.SuppressFinalize(this);
}