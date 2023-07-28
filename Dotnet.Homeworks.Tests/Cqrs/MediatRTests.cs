using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.Cqrs.Utils;
using Dotnet.Homeworks.MainProject.Controllers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using MediatR;
using Moq;

namespace Dotnet.Homeworks.Tests.Cqrs;

public class MediatRTests
{
    [Homework(RunLogic.Homeworks.Cqrs)]
    public async void Controller_ShouldHave_DependencyOnMediatR()
    {
        Mock<IMediator> mediatorMock = new Mock<IMediator>();
        var returns = new Result<List<object>>(null, true, null);
        mediatorMock.Setup(x => x.Send(It.IsAny<IQuery<List<object>>>(), new CancellationToken()))
            .ReturnsAsync(await giveData());
        
        // var returns = new object();
        // mediatorMock.Setup(x => x.Send(It.IsAny<object>(), It.IsAny<CancellationToken>()))
        //     .Returns(Task.FromResult(returns)!);
        
        var productManagementController = new ProductManagementController(mediatorMock.Object);
        
        var result = await productManagementController.GetProducts();
        
        mediatorMock.Verify(x=> 
            x.Send(It.IsAny<Result<List<object>>>(), new CancellationToken()), Times.Once);
    }

    public async Task<Result<List<object>>> giveData()
    {
        var returns = new Result<List<object>>(null, true, null);
        return returns;
    }
}