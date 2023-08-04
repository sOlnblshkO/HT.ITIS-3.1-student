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

public class CqrsTests
{
    [Homework(RunLogic.Homeworks.Cqrs)]
    public void CommandsAndQueries_Should_ImplementCertainInterface()
    {
        var getProductQueryType = typeof(GetProductsQuery);
        var getProductInterface = typeof(IQuery<List<GetProductsDto>>);

        var insertProductCommandType = typeof(InsertProductCommand);
        var insertProductInterface = typeof(ICommand<InsertProductDto>);

        var deleteProductCommandType = typeof(DeleteProductByGuidCommand);
        var deleteProductInterface = typeof(ICommand);

        var updateProductCommandType = typeof(UpdateProductCommand);
        var updateProductInterface = typeof(ICommand);
        
        Assert.Contains(getProductInterface, getProductQueryType.GetInterfaces());
        Assert.Contains(insertProductInterface, insertProductCommandType.GetInterfaces());
        Assert.Contains(deleteProductInterface, deleteProductCommandType.GetInterfaces());
        Assert.Contains(updateProductInterface, updateProductCommandType.GetInterfaces());
    }

    [Homework(RunLogic.Homeworks.Cqrs)]
    public void CommandAndQueryHandlers_Should_ImplementCertainInterfaces()
    {
        var productQueryType = typeof(GetProductsQueryHandler);
        var getProductInterface =
            typeof(IQueryHandler<,>).MakeGenericType(typeof(GetProductsQuery), typeof(List<GetProductsDto>));
        
        var insertProductType = typeof(InsertProductCommandHandler);
        var insertProductInterface =
            typeof(ICommandHandler<,>).MakeGenericType(typeof(InsertProductCommand), typeof(InsertProductDto));
        
        var deleteProductType = typeof(DeleteProductByGuidCommandHandler);
        var deleteProductInterface = typeof(ICommandHandler<>).MakeGenericType(typeof(DeleteProductByGuidCommand));
        
        var updateProductType = typeof(UpdateProductCommandHandler);
        var updateProductInterface = typeof(ICommandHandler<>).MakeGenericType(typeof(UpdateProductCommand));
        
        Assert.Contains(getProductInterface, productQueryType.GetInterfaces());
        Assert.Contains(insertProductInterface, insertProductType.GetInterfaces());
        Assert.Contains(deleteProductInterface, deleteProductType.GetInterfaces());
        Assert.Contains(updateProductInterface, updateProductType.GetInterfaces());
    }

    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task InsertOperation_IsCorrect()
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithHandlersInDi();
        var env = testEnvBuilder.Build();
        var insertCommand = TestProduct.GetInsertCommand();
        var getQuery = TestProduct.GetGetQuery();
        
        // Act
        var resultInsert =
            await CqrsEnvironment.HandleCommand<InsertProductCommand, InsertProductDto>(env.InsertCommandHandler,
                insertCommand);
        var resultGet = await CqrsEnvironment.HandleQuery<GetProductsQuery, List<GetProductsDto>>(env.GetQueryHandler, getQuery);

        // Assert
        Assert.True(resultInsert.IsSuccess);
        Assert.True(resultGet.IsSuccess);
        env.UnitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(resultGet.Value?
            .Single(x => x.Guid == resultInsert.Value?.Guid));
    }

    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task InsertOperation_Should_ReturnCorrectResponse()
    {
        // Arrange
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithHandlersInDi();
        var env = testEnvBuilder.Build();
        var insertCommand = TestProduct.GetInsertCommand();
        var getQuery = TestProduct.GetGetQuery();

        // Act
        await CqrsEnvironment.HandleCommand<InsertProductCommand, InsertProductDto>(env.InsertCommandHandler, insertCommand);
        var resultGet = await CqrsEnvironment.HandleQuery<GetProductsQuery, List<GetProductsDto>>(env.GetQueryHandler, getQuery);
        
        // Assert
        Assert.NotNull(resultGet.Value);
        Assert.Contains(resultGet.Value, p => p.Name == insertCommand.Name);
    }
}