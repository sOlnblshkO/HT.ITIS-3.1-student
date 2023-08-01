using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Features.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.Cqrs.Utils;
using Dotnet.Homeworks.MainProject.Controllers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Cqrs;
using MediatR;
using Moq;
using NetArchTest.Rules;

namespace Dotnet.Homeworks.Tests.Cqrs;

public class MediatorTests
{
    [Homework(RunLogic.Homeworks.Cqrs)]
    public void MediatR_Should_ResideInMainProject()
    {

        var result = Types.InNamespace(Constants.NamespaceMainProject)
            .Should()
            .HaveDependencyOn(Constants.MediatR)
            .GetResult();
        
        Assert.True(result.IsSuccessful);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallGetPoducts()
    {
        // just uncomment strokes below and delete exception                     
        throw new NotImplementedException();
        
        // var mediatorMock = new Mock<IMediator>();
        // var list = new List<GetProductDto>() { new GetProductDto(Guid.NewGuid(), "name") };
        // var returns = new Result<List<GetProductDto>>(list, true, null);
        // mediatorMock.Setup(x => x.Send(It.IsAny<IQuery<List<GetProductDto>>>(), It.IsAny<CancellationToken>()))
        //     .ReturnsAsync(returns);
        //
        // var productManagementController = new ProductManagementController(mediatorMock.Object);
        // 
        // var result = await productManagementController.GetProducts();
        // 
        // mediatorMock.Verify(x=> 
        //     x.Send(It.IsAny<IQuery<List<GetProductDto>>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallInsertPoduct()
    {
        // just uncomment strokes below and delete exception                     
        throw new NotImplementedException();
        
        // var mediatorMock = new Mock<IMediator>();
        // var dto = new InsertProductDto( Guid.NewGuid() );
        // var returns = new Result<InsertProductDto>(dto, true, null);
        // mediatorMock.Setup(x => x.Send(It.IsAny<ICommand<InsertProductDto>>(), It.IsAny<CancellationToken>()))
        //     .ReturnsAsync(returns);
        //
        // var productManagementController = new ProductManagementController(mediatorMock.Object);
        //
        // var result = await productManagementController.InsertProduct("Name");
        //
        // mediatorMock.Verify(x=> 
        //     x.Send(It.IsAny<ICommand<InsertProductDto>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallDeletePoduct()
    {
        // just uncomment strokes below and delete exception                     
        throw new NotImplementedException();
        
        // var mediatorMock = new Mock<IMediator>();
        // var returns = new Result(true, null);
        // mediatorMock.Setup(x => 
        //     x.Send(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()))
        //     .ReturnsAsync(returns);
        //
        // var productManagementController = new ProductManagementController(mediatorMock.Object);
        //
        // var result = await productManagementController.DeleteProduct(Guid.NewGuid());
        //
        // mediatorMock.Verify(x=> 
        //     x.Send(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallUpdatePoduct()
    {
        // just uncomment strokes below and delete exception                     
        throw new NotImplementedException();
        
        // var mediatorMock = new Mock<IMediator>();
        // var returns = new Result(true, null);
        // mediatorMock.Setup(x => 
        //         x.Send(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()))
        //     .ReturnsAsync(returns);
        //
        // var productManagementController = new ProductManagementController(mediatorMock.Object);
        //
        // var result = await productManagementController.UpdateProduct(Guid.NewGuid(), "Name");
        //
        // mediatorMock.Verify(x=> 
        //     x.Send(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}