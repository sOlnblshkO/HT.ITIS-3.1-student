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
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.MainProject.Controllers;
using Dotnet.Homeworks.Mediator;
using Moq;
using IRequest = Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.IRequest;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

internal class CqrsEnvironment
{
    public CqrsEnvironment(ProductManagementController productManagementController, UserManagementController userManagementController, Mock<IUnitOfWork> unitOfWorkMock, 
        Mock<MediatR.IMediator>? mediatRMock, Mock<Mediator.IMediator>? customMediatorMock, 
        InsertProductCommandHandler? insertProductCommandHandler, DeleteProductByGuidCommandHandler? deleteProductByGuidCommandHandler,
        UpdateProductCommandHandler? updateProductCommandHandler, GetProductsQueryHandler? getProductsQueryHandler,
        CreateUserCommandHandler? createUserCommandHandler, GetUserQueryHandler? getUserQueryHandler,
        UpdateUserCommandHandler? updateUserCommandHandler, DeleteUserCommandHandler? deleteUserCommandHandler, DeleteUserByAdminCommandHandler? deleteUserByAdminCommandHandler, GetAllUsersQueryHandler? getAllUsersQueryHandler,
        IMediator mediator = default)
    {
        Mediator = mediator;
        ProductManagementController = productManagementController;
        UserManagementController = userManagementController;
        InsertProductCommandHandler = insertProductCommandHandler;
        DeleteProductByGuidCommandHandler = deleteProductByGuidCommandHandler;
        UpdateProductCommandHandler = updateProductCommandHandler;
        GetProductsQueryHandler = getProductsQueryHandler;

        CreateUserCommandHandler = createUserCommandHandler;
        UnitOfWorkMock = unitOfWorkMock;
        MediatRMock = mediatRMock;
        CustomMediatorMock = customMediatorMock;
        GetUserQueryHandler = getUserQueryHandler;
        UpdateUserCommandHandler = updateUserCommandHandler;
        DeleteUserCommandHandler = deleteUserCommandHandler;
        DeleteUserByAdminCommandHandler = deleteUserByAdminCommandHandler;
        GetAllUsersQueryHandler = getAllUsersQueryHandler;
        
    }

    public ProductManagementController ProductManagementController { get; }
    public UserManagementController UserManagementController { get; }
    public Mock<IUnitOfWork> UnitOfWorkMock { get; }
    public Mock<MediatR.IMediator> MediatRMock { get; }
    public Mock<Mediator.IMediator> CustomMediatorMock { get; }
    public static IMediator Mediator { get; private set; }
    
    // Products
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public InsertProductCommandHandler? InsertProductCommandHandler { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public DeleteProductByGuidCommandHandler? DeleteProductByGuidCommandHandler { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public UpdateProductCommandHandler? UpdateProductCommandHandler { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public GetProductsQueryHandler? GetProductsQueryHandler { get; }
    
    // Users
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public GetUserQueryHandler? GetUserQueryHandler { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public CreateUserCommandHandler? CreateUserCommandHandler { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public DeleteUserCommandHandler? DeleteUserCommandHandler { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public UpdateUserCommandHandler? UpdateUserCommandHandler { get; }
    
    // UserManagement
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public DeleteUserByAdminCommandHandler? DeleteUserByAdminCommandHandler { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public GetAllUsersQueryHandler? GetAllUsersQueryHandler { get; }
    

    public static async Task<Result> HandleCommand<TCommand>(object? handler, TCommand command) =>
        await HandleGeneric<TCommand, Result>(handler, command);
    
    public static async Task<Result<TResponse>> HandleCommand<TCommand, TResponse>(object? handler, TCommand command) =>
        await HandleGeneric<TCommand, Result<TResponse>>(handler, command);

    public static async Task<Result<TResponse>> HandleQuery<TQuery, TResponse>(object? handler, TQuery query) =>
        await HandleGeneric<TQuery, Result<TResponse>>(handler, query);

    private static async Task<TResponse> HandleGeneric<TRequest, TResponse>(object? handler, TRequest request)
    {
        if (CqrsEnvironmentBuilder.IsCqrsComplete())
        {
            var type = Mediator.GetType();
            var methods = type.GetMethods();
            var method = methods?.First(
                    x=>x.GetGenericArguments().Length == 1 
                       && x.ReturnType.IsGenericType 
                       && x.Name == "Send")
                .MakeGenericMethod(typeof(TResponse));
            
            var resultFunc = await (Task<TResponse>)method?.Invoke(Mediator, new object[]{ request, new CancellationToken() });
            if (resultFunc is null || resultFunc is not TResponse result)
                throw new Exception($"The Handle method of the provided handler didn't return {nameof(TResponse)}");

            return result;
        }
        
        if (handler is null) throw new ArgumentNullException(nameof(handler));
        
        var handlerType = handler.GetType();
        var handleMethod = handlerType.GetMethod("Handle");
        if (handleMethod is null) throw new Exception("The provided handler does not contain method Handle.");

        var cancellationToken = new CancellationToken();
        var parameters = new object[]
            { request ?? throw new ArgumentNullException(nameof(request)), cancellationToken };
        if (handleMethod.Invoke(handler, parameters) is not TResponse handleResponse)
            throw new Exception(
                $"The Handle method of the provided handler didn't return {nameof(TResponse)}");
        return handleResponse;
    }

    public void VerifyMockedMediator<TInput>()
        where TInput : class
    {
        if (MediatRMock.Setups.Any())
            MediatRMock.Verify(x=> 
                x.Send(It.IsAny<TInput>(), It.IsAny<CancellationToken>()), Times.Once);
        
        else 
            CustomMediatorMock.Verify(x=> 
                x.Send(It.IsAny<TInput>(), It.IsAny<CancellationToken>()), Times.Once());
    }
}