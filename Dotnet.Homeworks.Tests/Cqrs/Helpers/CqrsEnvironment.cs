using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.MainProject.Controllers;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

internal class CqrsEnvironment
{
    public CqrsEnvironment(ProductManagementController productManagementController, IUnitOfWork unitOfWorkMock,
        MediatR.IMediator mediatR, Mediator.IMediator customMediator,
        IUserRepository userRepository)
    {
        ProductManagementController = productManagementController;
        CustomMediator = customMediator;
        UserRepository = userRepository;
        MediatR = mediatR;
        UnitOfWorkMock = unitOfWorkMock;
    }

    public ProductManagementController ProductManagementController { get; }
    public IUnitOfWork UnitOfWorkMock { get; }
    public MediatR.IMediator MediatR { get; }
    public Mediator.IMediator CustomMediator { get; }
    public IUserRepository UserRepository { get; }
}