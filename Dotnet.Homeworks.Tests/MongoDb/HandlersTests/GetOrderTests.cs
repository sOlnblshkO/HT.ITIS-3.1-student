using Dotnet.Homeworks.Tests.MongoDb.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using static Dotnet.Homeworks.Tests.Shared.TestRequests.TestOrder;
using static Dotnet.Homeworks.Tests.Shared.TestRequests.TestProduct;

namespace Dotnet.Homeworks.Tests.MongoDb.HandlersTests;

[Collection(nameof(RequestsAndHandlersImplementedFixture))]
public class GetOrderTests
{
    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldNotFind_Order_WithNotExistingId_WhenUserRegistered()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var order = await GetOrderAsync(env.Mediator, Guid.NewGuid());

        Assert.False(order.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFind_Order_WithExistingId_WhenUserRegistered()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);
        var order = await GetOrderAsync(env.Mediator, createdOrder.Value!.Id);

        Assert.True(order.IsSuccess);
        Assert.Equal(createdOrder.Value.Id, order.Value!.Id);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldNotFind_Order_OfOtherUser()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);
        await env.LogInNewUserAsync();
        var order = await GetOrderAsync(env.Mediator, createdOrder.Value!.Id);

        Assert.False(order.IsSuccess);
    }
}