using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.Shared.TestRequests;
using NSubstitute;

namespace Dotnet.Homeworks.Tests.Cqrs;

[Collection(nameof(AllProductsRequestsFixture))]
public class CqrsTests
{
    [Homework(RunLogic.Homeworks.Cqrs, true)]
    public async Task InsertOperation_IsCorrect()
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        // Act
        var resultInsert = await TestProduct.CreateProductAsync(env.MediatR);

        // Assert
        Assert.True(resultInsert.IsSuccess);
        await env.UnitOfWorkMock.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.Cqrs, true)]
    public async Task GetOperation_IsCorrect()
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var env = testEnvBuilder.Build();

        // Act
        var resultGet = await TestProduct.GetProductsAsync(env.MediatR);

        // Assert
        Assert.True(resultGet.IsSuccess);
        await env.UnitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}