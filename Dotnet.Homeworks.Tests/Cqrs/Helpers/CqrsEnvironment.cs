using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.MainProject.Controllers;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

internal class CqrsEnvironment
{
    public CqrsEnvironment(ProductManagementController productManagementController, UserManagementController userManagementController, IUnitOfWork unitOfWorkMock, 
        MediatR.IMediator mediatRMock, Mediator.IMediator customMediator)
    {
        ProductManagementController = productManagementController;
        UserManagementController = userManagementController;
        CustomMediatorMock = customMediator;
        MediatRMock = mediatRMock;
        UnitOfWorkMock = unitOfWorkMock;
    }

    public ProductManagementController ProductManagementController { get; }
    public UserManagementController UserManagementController { get; }
    public IUnitOfWork UnitOfWorkMock { get; }
    public MediatR.IMediator MediatRMock { get; }
    public Mediator.IMediator CustomMediatorMock { get; }
}