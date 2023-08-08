using Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;
using Dotnet.Homeworks.Features.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Moq;

namespace Dotnet.Homeworks.Tests.Cqrs;


[Collection(nameof(AllRequestsFixture))]
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
            await CqrsEnvironment.HandleCommand<InsertProductCommand, InsertProductDto>(env.InsertCommandHandler,
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
        var resultGet = await CqrsEnvironment.HandleQuery<GetProductsQuery, List<GetProductsDto>>(env.GetQueryHandler, getQuery);

        // Assert
        Assert.True(resultGet.IsSuccess);
        env.UnitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}