using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using NSubstitute;

namespace Dotnet.Homeworks.Tests.Cqrs;

[Collection(nameof(AllProductsRequestsFixture))]
public class CqrsTests
{
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task InsertOperation_IsCorrect()
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var env = testEnvBuilder.Build();
        var insertCommand = TestProduct.GetInsertCommand();

        // Act
        var resultInsert =
            await env.CustomMediatorMock.Send(insertCommand);

        // Assert
        Assert.True(resultInsert?.IsSuccess);
        await env.UnitOfWorkMock.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task GetOperation_IsCorrect()
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder();
        var env = testEnvBuilder.Build();
        var getQuery = TestProduct.GetGetQuery();

        // Act
        var resultGet =
            await env.CustomMediatorMock.Send(getQuery);

        // Assert
        Assert.True(resultGet?.IsSuccess);

        await env.UnitOfWorkMock.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}