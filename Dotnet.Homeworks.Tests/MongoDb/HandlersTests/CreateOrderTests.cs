using Dotnet.Homeworks.Tests.MongoDb.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using static Dotnet.Homeworks.Tests.Shared.TestRequests.TestOrder;
using static Dotnet.Homeworks.Tests.Shared.TestRequests.TestProduct;

namespace Dotnet.Homeworks.Tests.MongoDb.HandlersTests;

[Collection(nameof(RequestsAndHandlersImplementedFixture))]
public class CreateOrderTests
{
    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenUserNotRegistered()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);

        Assert.False(createdOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenUserNotExists()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder().WithFakeUserInContext();
        var env = testEnvBuilder.Build();

        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);

        Assert.False(createdOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenProductNotExists()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdOrder = await CreateOrderAsync(env.Mediator, Guid.NewGuid());

        Assert.False(createdOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldFail_WhenUserCreates_OrderWithNoProducts()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdOrder = await CreateOrderAsync(env.Mediator, Array.Empty<Guid>());

        Assert.False(createdOrder.IsSuccess);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldSucceed_WhenUser_CreatesOwnOrder()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct = await CreateProductAsync(env.Mediator);
        var createdOrder = await CreateOrderAsync(env.Mediator, createdProduct.Value!.Guid);

        Assert.True(createdOrder.IsSuccess);
        Assert.NotNull(createdOrder.Value);
        Assert.NotEqual(Guid.Empty, createdOrder.Value.Id);
    }

    [Homework(RunLogic.Homeworks.MongoDb)]
    public async Task ShouldCreate_Order_WhenUser_CreatesOwnOrder()
    {
        await using var testEnvBuilder = new MongoEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        await env.LogInNewUserAsync();
        var createdProduct1 = await CreateProductAsync(env.Mediator);
        var createdProduct2 = await CreateProductAsync(env.Mediator);
        var products = new[] { createdProduct1.Value!.Guid, createdProduct2.Value!.Guid };
        var createdOrder = await CreateOrderAsync(env.Mediator, products);
        var getOrder = await GetOrderAsync(env.Mediator, createdOrder.Value!.Id);

        Assert.True(createdOrder.IsSuccess);
        Assert.True(getOrder.IsSuccess);
        Assert.True(products.SequenceEqual(getOrder.Value!.ProductsIds));
    }
}