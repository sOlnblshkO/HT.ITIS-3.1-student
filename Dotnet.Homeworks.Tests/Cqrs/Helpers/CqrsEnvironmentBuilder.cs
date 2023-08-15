using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.DeleteProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.MainProject.Controllers;
using Dotnet.Homeworks.Tests.RunLogic.Utils.TestEnvironmentBuilder;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

internal class CqrsEnvironmentBuilder : TestEnvironmentBuilder<CqrsEnvironment>
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMediator> _mediatRMock = new();
    private ProductManagementController? _productManagementController;
    private InsertProductCommandHandler? _insertCommandHandler;
    private UpdateProductCommandHandler? _updateCommandHandler;
    private DeleteProductByGuidCommandHandler? _deleteCommandHandler;
    private GetProductsQueryHandler? _getQueryHandler;

    private bool _withMockedMediatR;
    private bool _withHanldersInDi;

    public CqrsEnvironmentBuilder WithMockedMediatR(bool withMockedMediatR = true)
    {
        _withMockedMediatR = withMockedMediatR;
        SetupMediatR();
        return this;
    }
    
    /// <summary>
    /// Should be used when need to get Handlers from container.
    /// </summary>
    public CqrsEnvironmentBuilder WithHandlersInDi(bool withHandlersInDi = true)
    {
        _withHanldersInDi = withHandlersInDi;
        return this;
    }
    
    public override void SetupServices(Action<IServiceCollection>? configureServices = default)
    {
        configureServices += s => s
            .AddSingleton<ProductManagementController>()
            .AddSingleton<IProductRepository, ProductRepositoryMock>()
            .AddSingleton(_unitOfWorkMock.Object);
        if (_withMockedMediatR) configureServices += s => s.AddSingleton(_mediatRMock);
        else
            configureServices += s => s.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Features.Helpers.AssemblyReference.Assembly));
        if (_withHanldersInDi)
            configureServices += s => s
                .AddSingleton<InsertProductCommandHandler>()
                .AddSingleton<UpdateProductCommandHandler>()
                .AddSingleton<DeleteProductByGuidCommandHandler>()
                .AddSingleton<GetProductsQueryHandler>();
        ServiceProvider = GetServiceProvider(configureServices);
    }

    public override CqrsEnvironment Build()
    {
        if (ServiceProvider is null) SetupServices();
        _productManagementController ??= ServiceProvider!.GetRequiredService<ProductManagementController>();
        if (!_withHanldersInDi)
            return new CqrsEnvironment(_productManagementController, _insertCommandHandler, _deleteCommandHandler,
                _updateCommandHandler, _getQueryHandler, _unitOfWorkMock, _mediatRMock);
        _insertCommandHandler ??= ServiceProvider!.GetRequiredService<InsertProductCommandHandler>();
        _updateCommandHandler ??= ServiceProvider!.GetRequiredService<UpdateProductCommandHandler>();
        _deleteCommandHandler ??= ServiceProvider!.GetRequiredService<DeleteProductByGuidCommandHandler>();
        _getQueryHandler ??= ServiceProvider!.GetRequiredService<GetProductsQueryHandler>();
        return new CqrsEnvironment(_productManagementController, _insertCommandHandler, _deleteCommandHandler,
            _updateCommandHandler, _getQueryHandler, _unitOfWorkMock, _mediatRMock);
    }

    private void SetupMediatR()
    {
        _mediatRMock.Setup(m => m.Send(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result(true, null));
        _mediatRMock.Setup(m => m.Send(It.IsAny<ICommand<InsertProductDto>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result<InsertProductDto>(new InsertProductDto(Guid.Empty), true, null));
        _mediatRMock.Setup(m => m.Send(It.IsAny<IQuery<List<GetProductsDto>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Result<List<GetProductsDto>>(new List<GetProductsDto>(), true, null));
    }
}