using System.Reflection;

namespace Dotnet.Homeworks.Mediator;

public partial class Mediator
{
    public static void RegisterHandlersFromAssembly(params Assembly[] assemblies)
    {
        var commandHandlerTypes = assemblies.SelectMany(a => a.GetTypes()
            .Where(type => type.GetInterfaces().Any(IsRequestHandlerInterface)));

        foreach (var commandHandlerType in commandHandlerTypes)
        {
            var interfaceType = commandHandlerType.GetInterfaces().Single(IsRequestHandlerInterface);
            RegisterHandler(interfaceType.GetGenericArguments()[0], commandHandlerType);
        }
    }
    
    private static bool IsRequestHandlerInterface(Type type)
    {
        return type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IRequestHandler<>) ||
                                      type.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
    }

    private static void RegisterHandler(Type commandType, Type handlerType)
    {
        if (!handlerType.GetInterfaces().Any(IsRequestHandlerInterface))
            throw new ArgumentException($"{handlerType.Name} does not implement IRequestHandler interface");

        Mediator.RequestHandlerMappings[commandType] = handlerType;
    }
}