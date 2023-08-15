using System.Reflection;
using System.Security.Claims;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.DeleteProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;
using Dotnet.Homeworks.Features.Cqrs.UserManagement.Commands.DeleteUserByAdmin;
using Dotnet.Homeworks.Features.Cqrs.UserManagement.Queries.GetAllUsers;
using Dotnet.Homeworks.Features.Cqrs.Users.Commands.CreateUser;
using Dotnet.Homeworks.Features.Cqrs.Users.Commands.DeleteUser;
using Dotnet.Homeworks.Features.Cqrs.Users.Commands.UpdateUser;
using Dotnet.Homeworks.Features.Cqrs.Users.Queries.GetUser;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.MainProject.Controllers;
using Dotnet.Homeworks.Tests.RunLogic.Utils.TestEnvironmentBuilder;
using Dotnet.Homeworks.Mediator.Helpers;
using Dotnet.Homeworks.Tests.CqrsValidation.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using MediatR;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

internal class CqrsEnvironmentBuilder : TestEnvironmentBuilder<CqrsEnvironment>
{
    public Mock<ProductRepositoryMock> ProductRepositoryMock { get; } = new Mock<ProductRepositoryMock>();
    public Mock<UserRepositoryMock> UserRepositoryMock { get; } = new Mock<UserRepositoryMock>();
    public Mock<IHttpContextAccessor> HttpContextAccessorMock { get; } = new Mock<IHttpContextAccessor>();

    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<MediatR.IMediator> _mediatRMock = new();
    private readonly Mock<Mediator.IMediator> _customMediatorMock = new();
    private ProductManagementController? _productManagementController;
    private UserManagementController? _userManagementController;
    private InsertProductCommandHandler? _insertProductCommandHandler;
    private UpdateProductCommandHandler? _updateProductCommandHandler;
    private DeleteProductByGuidCommandHandler? _deleteProductByGuidCommandHandler;
    private GetProductsQueryHandler? _getProductsQueryHandler;
    
    private CreateUserCommandHandler? _createUserCommandHandler;
    private DeleteUserCommandHandler? _deleteUserCommandHandler;
    private UpdateUserCommandHandler? _updateUserCommandHandler;
    private GetUserQueryHandler? _getUserQueryHandler;
    
    private DeleteUserByAdminCommandHandler? _deleteUserByAdminCommandHandler;
    private GetAllUsersQueryHandler? _getAllUsersQueryHandler;

    private bool _withMockedMediator;
    private bool _withHandlersInDi;
    private bool _withPipelineBehaviors;

    public CqrsEnvironmentBuilder WithMockedMediator(bool withMockedMediator = true)
    {
        _withMockedMediator = withMockedMediator;
        SetupMediator();
        return this;
    }
    
    /// <summary>
    /// Should be used when need to get Handlers from container.
    /// </summary>
    public CqrsEnvironmentBuilder WithHandlersInDi(bool withHandlersInDi = true)
    {
        _withHandlersInDi = withHandlersInDi;
        return this;
    }

    public CqrsEnvironmentBuilder WithPipelineBehaviors(bool withPipelineBehaviors = true)
    {
        _withPipelineBehaviors = withPipelineBehaviors;
        return this;
    }

    public override void SetupServices(Action<IServiceCollection>? configureServices = default)
    {
        configureServices += s => s
            .AddSingleton<ProductManagementController>()
            .AddSingleton<UserManagementController>()
            .AddSingleton<IProductRepository>(ProductRepositoryMock.Object)
            .AddSingleton<IUserRepository>(UserRepositoryMock.Object)
            .AddSingleton<IHttpContextAccessor>(HttpContextAccessorMock.Object)
            .AddSingleton(_unitOfWorkMock.Object)
            .AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
            .AddPermissionChecks(AppDomain.CurrentDomain.GetAssemblies());
        if (_withMockedMediator)
            configureServices += s =>
            {
                if (IsCqrsComplete()) s.AddSingleton(_customMediatorMock.Object);
                else s.AddSingleton(_mediatRMock.Object);
            };
        else
        {
            if (IsCqrsComplete())
                configureServices += s => s.AddMediator(Features.Helpers.AssemblyReference.Assembly);
            else
                configureServices += s => s.AddMediatR(cfg =>
                    cfg.RegisterServicesFromAssembly(Features.Helpers.AssemblyReference.Assembly));
        }

        if (_withHandlersInDi)
            configureServices += s => s
                .AddSingleton<InsertProductCommandHandler>()
                .AddSingleton<UpdateProductCommandHandler>()
                .AddSingleton<DeleteProductByGuidCommandHandler>()
                .AddSingleton<GetProductsQueryHandler>()
                
                .AddSingleton<CreateUserCommandHandler>()
                .AddSingleton<GetUserQueryHandler>()
                .AddSingleton<UpdateUserCommandHandler>()
                .AddSingleton<DeleteUserCommandHandler>()
                
                .AddSingleton<DeleteUserByAdminCommandHandler>()
                .AddSingleton<GetAllUsersQueryHandler>();

        if (_withPipelineBehaviors) // Порядок pipelineBehaviors может быть нарушен
        {
            var types = LoadPipelineBehavior();
            foreach (var type in types)
            {
                configureServices += s => s
                    .AddSingleton(typeof(Mediator.IPipelineBehavior<,>), type);   
            }
        }

        ServiceProvider = GetServiceProvider(configureServices);
    }

    public override CqrsEnvironment Build()
    {
        if (ServiceProvider is null) SetupServices();
        _productManagementController ??= ServiceProvider!.GetRequiredService<ProductManagementController>();
        _userManagementController ??= ServiceProvider!.GetRequiredService<UserManagementController>();
        if (!_withHandlersInDi)
            return new CqrsEnvironment(_productManagementController, _userManagementController, _unitOfWorkMock, _mediatRMock, _customMediatorMock,
                _insertProductCommandHandler, _deleteProductByGuidCommandHandler,
                _updateProductCommandHandler, _getProductsQueryHandler, _createUserCommandHandler,
                _getUserQueryHandler, _updateUserCommandHandler, _deleteUserCommandHandler,
                _deleteUserByAdminCommandHandler, _getAllUsersQueryHandler, ServiceProvider!.GetRequiredService<Mediator.IMediator>());
        
        _insertProductCommandHandler ??= ServiceProvider!.GetRequiredService<InsertProductCommandHandler>();
        _updateProductCommandHandler ??= ServiceProvider!.GetRequiredService<UpdateProductCommandHandler>();
        _deleteProductByGuidCommandHandler ??= ServiceProvider!.GetRequiredService<DeleteProductByGuidCommandHandler>();
        _getProductsQueryHandler ??= ServiceProvider!.GetRequiredService<GetProductsQueryHandler>();
        
        _createUserCommandHandler ??= ServiceProvider!.GetRequiredService<CreateUserCommandHandler>();
        _updateUserCommandHandler ??= ServiceProvider!.GetRequiredService<UpdateUserCommandHandler>();
        _deleteUserCommandHandler ??= ServiceProvider!.GetRequiredService<DeleteUserCommandHandler>();
        _getUserQueryHandler ??= ServiceProvider!.GetRequiredService<GetUserQueryHandler>();
        
        _deleteUserByAdminCommandHandler ??= ServiceProvider!.GetRequiredService<DeleteUserByAdminCommandHandler>();
        _getAllUsersQueryHandler ??= ServiceProvider!.GetRequiredService<GetAllUsersQueryHandler>();
        
        return new CqrsEnvironment(_productManagementController, _userManagementController, _unitOfWorkMock, _mediatRMock, _customMediatorMock, _insertProductCommandHandler, _deleteProductByGuidCommandHandler,
            _updateProductCommandHandler, _getProductsQueryHandler, _createUserCommandHandler,
            _getUserQueryHandler, _updateUserCommandHandler, _deleteUserCommandHandler,
            _deleteUserByAdminCommandHandler, _getAllUsersQueryHandler, ServiceProvider!.GetRequiredService<Mediator.IMediator>());
    }

    public void SetupHttpContextClaims(List<Claim> claims)
    {
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.SetupGet(x => x.User.Claims)
            .Returns(claims);
        
        HttpContextAccessorMock.SetupGet(x=>x.HttpContext).Returns(httpContextMock.Object);
    }

    private IEnumerable<Type> LoadPipelineBehavior()
    {
        var pipelineBehaviors = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.GetInterfaces().Any(x =>
                x.IsGenericType && x.GetGenericTypeDefinition() == typeof(Mediator.IPipelineBehavior<,>)));

        return pipelineBehaviors;
    }

    private void SetupMediator()
    {
        if (!IsCqrsComplete())
            SetupMediatR();
        else
            SetupCustomMediator();
    }

    private void SetupMediatR()
    {
        _mediatRMock.Setup(m => m.Send(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result(true, null));
        _mediatRMock.Setup(m => m.Send(It.IsAny<ICommand<InsertProductDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result<InsertProductDto>(new InsertProductDto(Guid.Empty), true, null));
        _mediatRMock.Setup(m => m.Send(It.IsAny<IQuery<GetProductsDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result<GetProductsDto>(new GetProductsDto(new List<GetProductDto>(){new GetProductDto(Guid.NewGuid(), "name")}), true, null));
    }

    private void SetupCustomMediator()
    {
        _customMediatorMock.Setup(m => m.Send(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result(true, "null"));
        _customMediatorMock.Setup(m => m.Send(It.IsAny<ICommand<InsertProductDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result<InsertProductDto>(new InsertProductDto(Guid.Empty), true, null));
        _mediatRMock.Setup(m => m.Send(It.IsAny<IQuery<GetProductsDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result<GetProductsDto>(new GetProductsDto(new List<GetProductDto>(){new GetProductDto(Guid.NewGuid(), "name")}), true, null));
    }

    internal static bool IsCqrsComplete()
    {
        var attrHomeworkProgress = typeof(HomeworkAttribute).Assembly.GetCustomAttributes<HomeworkProgressAttribute>().Single();
        var isCqrsComplete = attrHomeworkProgress.Number >= (int)RunLogic.Homeworks.CqrsValidatorsDecorators;
        return isCqrsComplete;
    }
}