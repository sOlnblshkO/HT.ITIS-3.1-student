using System.Reflection;
using Dotnet.Homeworks.Features.Helpers;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using NetArchTest.Rules;

namespace Dotnet.Homeworks.Tests.MongoDb.Helpers;

[CollectionDefinition(nameof(RequestsAndHandlersImplementedFixture))]
public class RequestsAndHandlersImplementedFixture : IDisposable,
    ICollectionFixture<RequestsAndHandlersImplementedFixture>
{
    private static readonly Assembly FeaturesAssembly = AssemblyReference.Assembly;
    
    public RequestsAndHandlersImplementedFixture()
    {
        if (!AreCommandsImplemented() || !AreQueriesImplemented() || !AreHandlersImplemented())
            throw new Exception($"Either commands, queries or handlers are not implemented in " +
                                $"{FeaturesAssembly.GetName().FullName} assembly");
    }

    private static bool AreHandlersImplemented() =>
        TypesWithEndingImplementSomeOfInterfaces("handler", typeof(ICommandHandler<>),
            typeof(ICommandHandler<,>), typeof(IQueryHandler<,>));

    private static bool AreQueriesImplemented() =>
        TypesWithEndingImplementSomeOfInterfaces("query", typeof(IQuery<>));

    private static bool AreCommandsImplemented() =>
        TypesWithEndingImplementSomeOfInterfaces("command", typeof(ICommand), typeof(ICommand<>));

    private static bool TypesWithEndingImplementSomeOfInterfaces(string typesEnding, params Type[] interfacesTypes)
    {
        if (interfacesTypes.Length == 0) return false;
        
        var types = Types
            .InAssembly(FeaturesAssembly)
            .That()
            .HaveNameEndingWith(typesEnding, StringComparison.InvariantCultureIgnoreCase);
        var conditions = types.Should().ImplementInterface(interfacesTypes[0]);
        for (var i = 1; i < interfacesTypes.Length; i++)
            conditions = conditions.Or().ImplementInterface(interfacesTypes[i]);
        return conditions.GetResult().IsSuccessful;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}