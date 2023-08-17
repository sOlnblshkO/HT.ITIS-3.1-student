using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.MainProject.Controllers;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

internal class CqrsEnvironment
{
    public CqrsEnvironment(ProductManagementController productManagementController, IUnitOfWork unitOfWorkMock,
        MediatR.IMediator mediatRMock, Mediator.IMediator customMediator,
        IUserRepository userRepository)
    {
        ProductManagementController = productManagementController;
        CustomMediatorMock = customMediator;
        UserRepository = userRepository;
        MediatRMock = mediatRMock;
        UnitOfWorkMock = unitOfWorkMock;
    }

    public ProductManagementController ProductManagementController { get; }
    public IUnitOfWork UnitOfWorkMock { get; }
    public MediatR.IMediator MediatRMock { get; }
    public Mediator.IMediator CustomMediatorMock { get; }
    public IUserRepository UserRepository { get; }
}