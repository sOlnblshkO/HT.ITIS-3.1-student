using Dotnet.Homeworks.Features.Cqrs.Products.Commands.DeleteProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.RunLogic.Utils.Cqrs;
using Moq;

namespace Dotnet.Homeworks.Tests.Cqrs;

[Collection(nameof(AllProductsRequestsFixture))]
public class MediatorTests
{
    // От это херни вообще нет смысла в 4 домашке. Если убрать AddMediatR будет ругаться. NetArch, оказывается, не работает c нугет либами
    [Homework(RunLogic.Homeworks.Cqrs)]
    public void MediatR_Should_ResideInMainProject()
    {
        // var res1 = Assembly.Load(Constants.MediatR);
        // var result2 = Types.InAssembly(res1)
        //     .Should()
        //     .ResideInNamespaceStartingWith(Constants.NamespaceMainProject)
        //     .GetResult();

        var result = MainProject.Helpers.AssemblyReference.Assembly
            .GetReferencedAssemblies()
            .FirstOrDefault(x=>x.Name == Constants.MediatR);

        Assert.NotNull(result);
    }
    
    // Пытался инкапсулировать этот if в env.VerifyMockedMediator<TRequest>, но он не проходил, хотя тип абсолютно идентичен тому, что засетапен в envBuilder'e
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallGetProducts()
    {
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediator();
        var env = testEnvBuilder.Build();

        await env.ProductManagementController.GetProducts();
        
        // env.VerifyMockedMediator<IQuery<GetProductsDto>>(); - не пройдет 

        if (CqrsEnvironmentBuilder.IsCqrsComplete()) 
            env.CustomMediatorMock.Verify(x=> 
                x.Send(It.IsAny<IQuery<GetProductsDto>>(), It.IsAny<CancellationToken>()), Times.Once);
        else 
            env.MediatRMock.Verify(x=> 
                x.Send(It.IsAny<IQuery<GetProductsDto>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallInsertProduct()
    {   
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediator();
        var env = testEnvBuilder.Build();
        
        await env.ProductManagementController.InsertProduct("Name");
        
        if (CqrsEnvironmentBuilder.IsCqrsComplete()) 
            env.CustomMediatorMock.Verify(x=> 
                x.Send(It.IsAny<ICommand<InsertProductDto>>(), It.IsAny<CancellationToken>()), Times.Once);
        else 
            env.MediatRMock.Verify(x=> 
                x.Send(It.IsAny<ICommand<InsertProductDto>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallDeleteProduct()
    {   
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediator();
        var env = testEnvBuilder.Build();
        
        await env.ProductManagementController.DeleteProduct(Guid.NewGuid());
        
        if (CqrsEnvironmentBuilder.IsCqrsComplete()) 
            env.CustomMediatorMock.Verify(x=> 
                x.Send(It.IsAny<DeleteProductByGuidCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        else 
            env.MediatRMock.Verify(x=> 
                x.Send(It.IsAny<DeleteProductByGuidCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async Task Controller_Should_CallMediator_WhenCallUpdateProduct()
    {   
        await using var testEnvBuilder = new CqrsEnvironmentBuilder().WithMockedMediator();
        var env = testEnvBuilder.Build();
        
        await env.ProductManagementController.UpdateProduct(Guid.NewGuid(), "Name");

        if (CqrsEnvironmentBuilder.IsCqrsComplete()) 
            env.CustomMediatorMock.Verify(x=> 
                x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        else 
            env.MediatRMock.Verify(x=> 
                x.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}