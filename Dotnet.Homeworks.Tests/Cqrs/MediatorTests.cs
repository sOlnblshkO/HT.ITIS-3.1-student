using Dotnet.Homeworks.Features.Cqrs.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Cqrs;
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
    public async Task Controller_Should_CallMediator_WhenCallGetProducts()
    {
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediatR();
        var env = testEnvBuilder.Build();

        await env.ProductManagementController.GetProducts();
        
        env.MediatRMock.Verify(x=> 
            x.Send(It.IsAny<IQuery<List<GetProductsDto>>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallInsertProduct()
    {   
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediatR();
        var env = testEnvBuilder.Build();
        
        await env.ProductManagementController.InsertProduct("Name");
        
        env.MediatRMock.Verify(x=> 
            x.Send(It.IsAny<ICommand<InsertProductDto>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallDeleteProduct()
    {   
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediatR();
        var env = testEnvBuilder.Build();
        
        await env.ProductManagementController.DeleteProduct(Guid.NewGuid());
        
        env.MediatRMock.Verify(x=> 
            x.Send(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallUpdateProduct()
    {   
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediatR();
        var env = testEnvBuilder.Build();
        
        await env.ProductManagementController.UpdateProduct(Guid.NewGuid(), "Name");
        
        env.MediatRMock.Verify(x=> 
            x.Send(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}