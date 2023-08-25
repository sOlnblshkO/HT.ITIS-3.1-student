using Dotnet.Homeworks.Tests.MongoDb.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using static Dotnet.Homeworks.Tests.Shared.TestRequests.TestOrder;
using static Dotnet.Homeworks.Tests.Shared.TestRequests.TestProduct;

namespace Dotnet.Homeworks.Tests.MongoDb.HandlersTests;

[Collection(nameof(RequestsAndHandlersImplementedFixture))]
public class UpdateOrderTests
{
    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenUserNotRegistered()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct1 = await CreateProductAsync(env.Mediator);
        var createdProduct2 = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct1.Value!.Guid);
        env.LogOutCurrentUser();
        var updatedOrder =
            await UpdateOrderAsync(env.Mediator, createdOrder.Value!.Id, new[] { createdProduct2.Value!.Guid });

        Assert.False(updatedOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenUser_UpdatesOtherUsersOrder()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var productRes1 = await CreateProductAsync(env.Mediator);
        var productRes2 = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, productRes1.Value!.Guid);
        await env.LogInNewUserAsync();
        var updatedOrder =
            await UpdateOrderAsync(env.Mediator, createdOrder.Value!.Id, new[] { productRes2.Value!.Guid });

        Assert.False(updatedOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenUser_UpdatesOrder_WithNonExistingNewProduct()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);
        var updatedOrder =
            await UpdateOrderAsync(env.Mediator, createdOrder.Value!.Id, new[] { Guid.NewGuid() });

        Assert.False(updatedOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenUser_UpdatesOrder_WithEmptyNewProducts()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);
        var updatedOrder = await UpdateOrderAsync(env.Mediator, createdOrder.Value!.Id, Array.Empty<Guid>());

        Assert.False(updatedOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenOrderNotExists()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder =
            await UpdateOrderAsync(env.Mediator, Guid.NewGuid(), new[] { createdProduct.Value!.Guid });

        Assert.False(createdOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldSucceed_WhenUser_UpdatesOwnOrder()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct1 = await CreateProductAsync(env.Mediator);
        var createdProduct2 = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct1.Value!.Guid);
        var updatedOrder =
            await UpdateOrderAsync(env.Mediator, createdOrder.Value!.Id, new[] { createdProduct2.Value!.Guid });

        Assert.True(updatedOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldUpdate_Order_WhenUser_UpdatesOwnOrder()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var productRes1 = await CreateProductAsync(env.Mediator);
        var productRes2 = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, productRes1.Value!.Guid);
        var updatedOrder =
            await UpdateOrderAsync(env.Mediator, createdOrder.Value!.Id, new[] { productRes2.Value!.Guid });
        var getOrder = await GetOrderAsync(env.Mediator, createdOrder.Value!.Id);

        Assert.True(updatedOrder.IsSuccess);
        Assert.True(getOrder.IsSuccess);
        Assert.Equal(productRes2.Value!.Guid, getOrder.Value!.ProductsIds.Single());
    }
}