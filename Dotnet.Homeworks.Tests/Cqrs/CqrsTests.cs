using Dotnet.Homeworks.Application.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;
using Dotnet.Homeworks.Features.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.MessagingContracts.FeaturesContracts;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Moq;

namespace Dotnet.Homeworks.Tests.Cqrs;

public class CqrsTests
{
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public CqrsTests()
    {
        _unitOfWorkMock = new();
        _productRepositoryMock = new();
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public void CommandsAndQueries_Should_ImplementCertainInterface()
    {
        var getProductQueryType = typeof(GetProductsQuery);
        var getProductInterface = typeof(IQuery<List<GetProductDto>>);

        var insertProductCommandType = typeof(InsertProductCommand);
        var insertProductInterface = typeof(ICommand<InsertProductDto>);

        var deleteProductCommandType = typeof(DeleteProductByIdCommand);
        var deleteProductInterface = typeof(ICommand);

        var updateProductCommandType = typeof(UpdateProductCommand);
        var updateProductInterface = typeof(ICommand);
        
        Assert.True(getProductQueryType?.GetInterfaces().Contains(getProductInterface));
        Assert.True(insertProductCommandType?.GetInterfaces().Contains(insertProductInterface));
        Assert.True(deleteProductCommandType?.GetInterfaces().Contains(deleteProductInterface));
        Assert.True(updateProductCommandType?.GetInterfaces().Contains(updateProductInterface));
    }

    [Homework(RunLogic.Homeworks.Cqrs)]
    public void CommandAndQueryHandlers_Should_ImplementCertainInterfaces()
    {
        // just uncomment strokes below and delete exception    
        throw new NotImplementedException();
        
        // var productQueryType = typeof(GetProductsHandler);
        // var getProductInterface = typeof(IQueryHandler<GetProductsQuery, List<GetProductDto>>);
        //
        // var insertProductType = typeof(InsertProductHandler);
        // var insertProductInterface = typeof(ICommandHandler<InsertProductCommand, InsertProductDto>);
        //
        // var deleteProductType = typeof(DeleteProductByIdHandler);
        // var deleteProductInterface = typeof(ICommandHandler<DeleteProductByIdCommand>);
        //
        // var updateProductType = typeof(UpdateProductHandler);
        // var updateProductInterface = typeof(ICommandHandler<UpdateProductCommand>);
        //
        // Assert.True(productQueryType?.GetInterfaces().Contains(getProductInterface));
        // Assert.True(insertProductType?.GetInterfaces().Contains(insertProductInterface));
        // Assert.True(deleteProductType?.GetInterfaces().Contains(deleteProductInterface));
        // Assert.True(updateProductType?.GetInterfaces().Contains(updateProductInterface));
    }

    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task InsertOperation_IsCorrect()
    {
        // just uncomment strokes below and delete exception    
        throw new NotImplementedException();
        
        // // Arrange     
        // var productList = new List<Product>();
        // var product = new Product(){  Id = Guid.NewGuid(), Name = "Name" };
        // _productRepositoryMock.Setup(
        //     x => x.InsertProductAsync(
        //         It.IsAny<Product>()
        //     )
        // ).ReturnsAsync(() =>
        // {
        //     productList.Add(product);
        //     return product.Id;
        // });
        // _productRepositoryMock.Setup(
        //     x => x.GetAllProductsAsync()
        // ).ReturnsAsync(productList);
        //
        // var command = new InsertProductCommand(product.Name);
        // var commandHandler = new InsertProductHandler(_productRepositoryMock.Object,
        //     _unitOfWorkMock.Object);
        //
        // var query = new GetProductsQuery(); 
        // var queryHandler = new GetProductsHandler(_productRepositoryMock.Object,
        //     _unitOfWorkMock.Object);
        //
        // // Act
        // var resultInsert = await commandHandler.Handle(command);
        // var resultGet = await queryHandler.Handle(query, new CancellationToken());
        //
        // // Assert
        // Assert.True(resultInsert.IsSuccess);
        // Assert.True(resultGet.IsSuccess);
        // _unitOfWorkMock.Verify(x=>x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        // Assert.NotNull(resultGet?.Value?
        //     .Single(x=>x.Id == resultInsert?.Value?.Id));
    }

    [Homework(RunLogic.Homeworks.Cqrs)]
    public async void InsertOperation_Should_ReturnCorrectResponse()    
    {
        // just uncomment strokes below and delete exception                     
        throw new NotImplementedException();                                      
    
        // // Arrange                                
        // var product = new Product() { Id = Guid.NewGuid(), Name = "Name" };
        // _productRepositoryMock.Setup(x => 
        //     x.InsertProductAsync(It.IsAny<Product>())).ReturnsAsync(product.Id);
        //
        // var command = new InsertProductCommand(product.Name);
        // var commandHandler = new InsertProductHandler(_productRepositoryMock.Object,
        //     _unitOfWorkMock.Object);
        //
        // // Act                                                                       
        // var resultInsert = await commandHandler.Handle(command);
        //
        // // Assert
        // Assert.True(resultInsert.IsSuccess);
        // Assert.Equal(product.Id, resultInsert?.Value?.Id);
    }
}