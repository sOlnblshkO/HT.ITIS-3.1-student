using Dotnet.Homeworks.Tests.MongoDb.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using static Dotnet.Homeworks.Tests.Shared.TestRequests.TestOrder;
using static Dotnet.Homeworks.Tests.Shared.TestRequests.TestProduct;

namespace Dotnet.Homeworks.Tests.MongoDb.HandlersTests;

[Collection(nameof(RequestsAndHandlersImplementedFixture))]
public class DeleteOrderTests
{
    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenUserNotRegistered()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);
        env.LogOutCurrentUser();
        var deletedOrder = await DeleteOrderAsync(env.Mediator, createdOrder.Value!.Id);

        Assert.False(deletedOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenUser_DeletesOtherUsersOrder()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);
        await env.LogInNewUserAsync();
        var deletedOrder = await DeleteOrderAsync(env.Mediator, createdOrder.Value!.Id);

        Assert.False(deletedOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldSucceed_WhenUser_DeletesNotExistingOrder()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var deletedOrder = await DeleteOrderAsync(env.Mediator, Guid.NewGuid());

        Assert.True(deletedOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldSucceed_WhenUser_DeletesOwnOrder()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);
        var deletedOrder = await DeleteOrderAsync(env.Mediator, createdOrder.Value!.Id);

        Assert.True(deletedOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldDeleteOrder_WhenUser_DeletesOwnOrder()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);
        var deletedOrder = await DeleteOrderAsync(env.Mediator, createdOrder.Value!.Id);
        var order = await GetOrderAsync(env.Mediator, createdOrder.Value!.Id);

        Assert.True(deletedOrder.IsSuccess);
        Assert.False(order.IsSuccess);
    }
}