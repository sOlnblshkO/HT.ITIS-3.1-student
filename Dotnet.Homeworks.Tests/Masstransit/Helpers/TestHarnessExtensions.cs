using MassTransit.Testing;

namespace Dotnet.Homeworks.Tests.Masstransit.Helpers;

public static class TestHarnessExtensions
{
    public static object GetConsumerHarness<T>(this ITestHarness harness) where T : class
    {
        var getConsumerHarnessMethod = typeof(ITestHarness).GetMethod("GetConsumerHarness");
        if (getConsumerHarnessMethod is null)
            throw new Exception("ITestHarness API has changed. There is now not such a method as GetConsumerHarness");
        var getConsumerHarnessGenericMethod = getConsumerHarnessMethod.MakeGenericMethod(typeof(T));
        var consumer = getConsumerHarnessGenericMethod.Invoke(harness, null);
        if (consumer is null)
            throw new ArgumentException(
                $"Provided ITestHarness instance does not contain a declaration of a consumer with the specified type {nameof(T)}");
        return consumer;
    }
}