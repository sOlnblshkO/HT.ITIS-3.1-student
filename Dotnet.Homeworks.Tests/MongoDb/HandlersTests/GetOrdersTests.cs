using Dotnet.Homeworks.Tests.MongoDb.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using static Dotnet.Homeworks.Tests.Shared.TestRequests.TestOrder;
using static Dotnet.Homeworks.Tests.Shared.TestRequests.TestProduct;

namespace Dotnet.Homeworks.Tests.MongoDb.HandlersTests;

[Collection(nameof(RequestsAndHandlersImplementedFixture))]
public class GetOrdersTests
{
    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenUserUnregistered()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        var orders = await GetOrdersAsync(env.Mediator);

        Assert.False(orders.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldReturn_EmptyOrders_WhenUserNotExists()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder().WithFakeUserInContext();
        var env = testEnvBuilder.Build();

        var orders = await GetOrdersAsync(env.Mediator);

        Assert.True(orders.IsSuccess);
        Assert.Empty(orders.Value!.Orders);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldReturn_CorrectOrders_WhenUserExists()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder1 = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);
        var createdOrder2 = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);
        var orders = await GetOrdersAsync(env.Mediator);

        var ordersList = orders.Value?.Orders.Select(o => o.Id).ToList();
        Assert.True(orders.IsSuccess);
        Assert.Equal(2, ordersList!.Count);
        Assert.Contains(createdOrder1.Value!.Id, ordersList);
        Assert.Contains(createdOrder2.Value!.Id, ordersList);
    }
}