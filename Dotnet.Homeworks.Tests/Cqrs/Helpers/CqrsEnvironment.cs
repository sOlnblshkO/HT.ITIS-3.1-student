using Dotnet.Homeworks.Features.Cqrs.Products.Commands.DeleteProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.MainProject.Controllers;
using MediatR;
using Moq;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

internal class CqrsEnvironment
{
    public CqrsEnvironment(ProductManagementController productManagementController,
        InsertProductCommandHandler? insertCommandHandler, DeleteProductByGuidCommandHandler? deleteCommandHandler,
        UpdateProductCommandHandler? updateCommandHandler, GetProductsQueryHandler? getQueryHandler,
        Mock<IUnitOfWork> unitOfWorkMock, Mock<IMediator> mediatRMock)
    {
        ProductManagementController = productManagementController;
        InsertCommandHandler = insertCommandHandler;
        DeleteCommandHandler = deleteCommandHandler;
        UpdateCommandHandler = updateCommandHandler;
        GetQueryHandler = getQueryHandler;
        UnitOfWorkMock = unitOfWorkMock;
        MediatRMock = mediatRMock;
    }

    public ProductManagementController ProductManagementController { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public InsertProductCommandHandler? InsertCommandHandler { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public DeleteProductByGuidCommandHandler? DeleteCommandHandler { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public UpdateProductCommandHandler? UpdateCommandHandler { get; }
    /// <summary>
    /// null if builder wasn't called WithHandlersInDi.
    /// </summary>
    public GetProductsQueryHandler? GetQueryHandler { get; }
    public Mock<IUnitOfWork> UnitOfWorkMock { get; }
    public Mock<IMediator> MediatRMock { get; }
    
    public static async Task<Result> HandleCommand<TCommand>(object? handler, TCommand command) =>
        await HandleGeneric<TCommand, Task<Result>>(handler, command);
    
    public static async Task<Result<TResponse>> HandleCommand<TCommand, TResponse>(object? handler, TCommand command) =>
        await HandleGeneric<TCommand, Task<Result<TResponse>>>(handler, command);

    public static async Task<Result<TResponse>> HandleQuery<TQuery, TResponse>(object? handler, TQuery query) =>
        await HandleGeneric<TQuery, Task<Result<TResponse>>>(handler, query);

    private static TResponse HandleGeneric<TRequest, TResponse>(object? handler, TRequest request)
    {
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
}