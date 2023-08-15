using Dotnet.Homeworks.Features.Cqrs.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;
using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Moq;

namespace Dotnet.Homeworks.Tests.Cqrs;

[Collection(nameof(AllProductsRequestsFixture))]
public class CqrsTests
{
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task InsertOperation_IsCorrect()
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithHandlersInDi();
        var env = testEnvBuilder.Build();
        var insertCommand = TestProduct.GetInsertCommand();
        
        // Act
        var resultInsert =
            await CqrsEnvironment.HandleCommand<InsertProductCommand, InsertProductDto>(env.InsertProductCommandHandler,
                insertCommand);

        // Assert
        Assert.True(resultInsert.IsSuccess);
        env.UnitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task GetOperation_IsCorrect()
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithHandlersInDi();
        var env = testEnvBuilder.Build();
        var getQuery = TestProduct.GetGetQuery();
        
        // Act
        var resultGet = await CqrsEnvironment.HandleQuery<GetProductsQuery, GetProductsDto>(env.GetProductsQueryHandler, getQuery);

        // Assert
        Assert.True(resultGet.IsSuccess);
        env.UnitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task DeleteOperation_Should_ReturnFailedResult_WhenNoSuchProductExists()
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithHandlersInDi();
        var env = testEnvBuilder.Build();
        var getDelete = TestProduct.GetDeleteCommand(Guid.NewGuid());
        
        // Act
        var result = await CqrsEnvironment.HandleCommand(env.DeleteProductByGuidCommandHandler, getDelete);

        // Assert
        Assert.True(result.IsFailure);
        env.UnitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}