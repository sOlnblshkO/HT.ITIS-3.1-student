using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.Cqrs.Utils;
using Dotnet.Homeworks.MainProject.Controllers;
using Dotnet.Homeworks.MessagingContracts.FeaturesContracts;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using MediatR;
using Moq;
using NetArchTest.Rules;

namespace Dotnet.Homeworks.Tests.Cqrs;

public class MediatorTests
{
    [Homework(RunLogic.Homeworks.Cqrs)]
    public void MediatR_Should_ResideInMainProject()
    {
        var mediatR = "MediatR";
        
        var result = Types.InNamespace("Dotnet.Homeworks.MainProject")
            .Should()
            .HaveDependencyOn(mediatR)
            .GetResult();
        
        Assert.True(result.IsSuccessful);
    }
    
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async void Controller_Should_CallMediator()
    {
        var mediatorMock = new Mock<IMediator>();
        var list = new List<GetProductDto>() { new GetProductDto(Guid.NewGuid(), "name") };
        var returns = new Result<List<GetProductDto>>(list, true, null);
        mediatorMock.Setup(x => x.Send(It.IsAny<IQuery<List<GetProductDto>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(returns);

        var productManagementController = new ProductManagementController(mediatorMock.Object);
        
        var result = await productManagementController.GetProducts();
        
        mediatorMock.Verify(x=> 
            x.Send(It.IsAny<IQuery<List<GetProductDto>>>(), It.IsAny<CancellationToken>()), Times.Once);
    }    
}