using System.Reflection;
using Dotnet.Homeworks.MessagingContracts.Email;
using MassTransit.Testing;

namespace Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;

internal static class ReflectionHelpers
{
    private static PropertyInfo GetConsumedPropertyInfo<T>(object consumer)
    {
        var consumerType = consumer.GetType();
        var iTestHarnessConsumerType = consumerType.GetInterfaces().FirstOrDefault(i =>
            i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IConsumerTestHarness<>));
        if (iTestHarnessConsumerType is null)
            throw new ArgumentException($"Provided consumer does not implement IConsumer<{nameof(T)}> interface");
        var consumedProperty = iTestHarnessConsumerType.GetProperty("Consumed");
        if (consumedProperty is null)
            throw new Exception("IConsumer<TMessage> API has changed. There is now not such a property as Consumed");
        return consumedProperty;
    }

    internal static async Task<bool> AnyConsumedMessagesWithFilterAsync<T>(object consumer,
        FilterDelegate<IReceivedMessage<T>>? filter = default, CancellationToken cancellationToken = default) where T : class
    {
        filter ??= _ => true;
        var consumedProperty = GetConsumedPropertyInfo<T>(consumer);
        var iReceivedMessageListType = consumedProperty.PropertyType;
        var filterDelegateGenericType = typeof(FilterDelegate<>).MakeGenericType(typeof(IReceivedMessage<>));
        var anyMethod = iReceivedMessageListType.GetMethods().FirstOrDefault(m =>
        {
            if (m.Name != "Any") return false;
            var p = m.GetParameters();
            return p.Length == 2 && p[0].ParameterType.FullName == filterDelegateGenericType.FullName
                                 && p[1].ParameterType.FullName == typeof(CancellationToken).FullName;
        });
        if (anyMethod is null)
            throw new Exception("IReceivedMessageList API has changed. There is now not such a method as Any with provided parameter types");
        var anyGenericMethod = anyMethod.MakeGenericMethod(typeof(SendEmail));
        var parameters = new object?[] { filter, cancellationToken };
        var consumedPropertyValue = consumedProperty.GetValue(consumer)!;
        var resObj = anyGenericMethod.Invoke(consumedPropertyValue, parameters);
        return resObj is bool res ? res : await (resObj as Task<bool>)!;
    }

    internal static int CountConsumedMessages<T>(object consumer)
    {
        var consumedProperty = GetConsumedPropertyInfo<T>(consumer);
        var consumedPropertyValue = consumedProperty.GetValue(consumer)!;
        var assembly = Assembly.GetAssembly(consumedPropertyValue.GetType());
        if (assembly is null)
            throw new Exception("For whatever reason, assembly is null. God bless you");
        var countParameterTypes = new[] { typeof(IAsyncElementList<>), typeof(CancellationToken) };
        var countMethod = assembly.GetTypes()
            .FirstOrDefault(t => t.FullName == typeof(AsyncElementListExtensions).FullName)?
            .GetMethods()
            .FirstOrDefault(m =>
            {
                if (m.Name != "Count") return false;
                var p = m.GetParameters();
                return p.Length == 2 && p[0].ParameterType.ToString() == countParameterTypes[0].ToString()
                                     && p[1].ParameterType.ToString() == countParameterTypes[1].ToString();
            });
        if (countMethod is null)
            throw new Exception("AsyncElementListExtensions has changed. There is now not such a method as Count with provided parameter types");
        var countGenericMethod = countMethod.MakeGenericMethod(typeof(IReceivedMessage));
        var resObj = countGenericMethod.Invoke(null, new[] { consumedPropertyValue, default(CancellationToken) });
        return resObj is int res ? res : throw new Exception("A signature of Count method has changed. It does not return int now");
    }
}