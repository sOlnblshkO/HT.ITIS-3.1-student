using System.Reflection;
using System.Security.Claims;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Features.Cqrs.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Cqrs.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker;
using Dotnet.Homeworks.Infrastructure.Services.PermissionChecker.DependencyInjectionExtensions;
using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.MainProject.Controllers;
using Dotnet.Homeworks.Mediator.DependencyInjectionExtensions;
using Dotnet.Homeworks.Tests.RunLogic.Utils.TestEnvironmentBuilder;
using Dotnet.Homeworks.Mediator.Helpers;
using Dotnet.Homeworks.Tests.CqrsValidation.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NSubstitute;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

internal class CqrsEnvironmentBuilder : TestEnvironmentBuilder<CqrsEnvironment>
{
    public ProductRepositoryMock ProductRepositoryMock { get; } = Substitute.For<ProductRepositoryMock>();
    public UserRepositoryMock UserRepositoryMock { get; } = Substitute.For<UserRepositoryMock>();
    public IHttpContextAccessor HttpContextAccessorMock { get; } = Substitute.For<IHttpContextAccessor>();

    private IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();
    private MediatR.IMediator _mediatRMock = Substitute.For<MediatR.IMediator>();
    private Mediator.IMediator _customMediatorMock = Substitute.For<Mediator.IMediator>();
    private ProductManagementController? _productManagementController;

    private bool _withMockedMediator;
    private bool _withPipelineBehaviors;

    public CqrsEnvironmentBuilder WithMockedMediator(bool withMockedMediator = true)
    {
        _withMockedMediator = withMockedMediator;
        SetupMediatorMock();
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
            .AddSingleton<IProductRepository>(ProductRepositoryMock)
            .AddSingleton<IUserRepository>(UserRepositoryMock)
            .AddSingleton(HttpContextAccessorMock)
            .AddSingleton(_unitOfWork)
            .AddValidatorsFromAssembly(Features.Helpers.AssemblyReference.Assembly)
            .AddPermissionChecks(Features.Helpers.AssemblyReference.Assembly);

        SetupMediator(ref configureServices);

        SetupPipelineBehavior(ref configureServices);

        ServiceProvider = GetServiceProvider(configureServices);
    }

    public override CqrsEnvironment Build()
    {
        if (ServiceProvider is null) SetupServices();
        _productManagementController ??= ServiceProvider!.GetRequiredService<ProductManagementController>();
        GetMockedMediatorFromServiceProvider();

        return new CqrsEnvironment(_productManagementController,
            _unitOfWork, _mediatRMock, _customMediatorMock, ProductRepositoryMock, UserRepositoryMock);
    }

    public void SetupHttpContextClaims(List<Claim> claims)
    {
        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.SetupGet(x => x.User.Claims)
            .Returns(claims);

        HttpContextAccessorMock.HttpContext.Returns(httpContextMock.Object);
    }

    private IEnumerable<Type> LoadPipelineBehavior()
    {
        var pipelineBehaviors = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.GetInterfaces().Any(x =>
                x.IsGenericType && x.GetGenericTypeDefinition() == typeof(Mediator.IPipelineBehavior<,>)));

        return pipelineBehaviors;
    }

    private void SetupMediatorMock()
    {
        if (!IsCqrsComplete())
            SetupMediatRMock();
        else
            SetupCustomMediatorMock();
    }

    private void SetupMediatRMock()
    {
        _mediatRMock.Send(Arg.Any<ICommand>(), Arg.Any<CancellationToken>())
            .Returns(new Result(true));
        _mediatRMock.Send(Arg.Any<ICommand<InsertProductDto>>(), Arg.Any<CancellationToken>())
            .Returns(new Result<InsertProductDto>(new InsertProductDto(Guid.Empty), true));
        _mediatRMock.Send(Arg.Any<IQuery<GetProductsDto>>(), Arg.Any<CancellationToken>())
            .Returns(new Result<GetProductsDto>(
                new GetProductsDto(new List<GetProductDto>() { new GetProductDto(Guid.NewGuid(), "name") }), true));
    }

    private void SetupCustomMediatorMock()
    {
        _customMediatorMock.Send(Arg.Any<ICommand>(), Arg.Any<CancellationToken>())
            .Returns(new Result(true));
        _customMediatorMock.Send(Arg.Any<ICommand<InsertProductDto>>(), Arg.Any<CancellationToken>())
            .Returns(new Result<InsertProductDto>(new InsertProductDto(Guid.Empty), true));
        _customMediatorMock.Send(Arg.Any<IQuery<GetProductsDto>>(), Arg.Any<CancellationToken>())
            .Returns(new Result<GetProductsDto>(
                new GetProductsDto(new List<GetProductDto>() { new GetProductDto(Guid.NewGuid(), "name") }), true));
    }

    private static bool IsCqrsComplete()
    {
        var attrHomeworkProgress =
            typeof(HomeworkAttribute).Assembly.GetCustomAttributes<HomeworkProgressAttribute>().Single();
        var isCqrsComplete = attrHomeworkProgress.Number >= (int)RunLogic.Homeworks.CqrsValidatorsDecorators;
        return isCqrsComplete;
    }

    private void SetupMediator(ref Action<IServiceCollection>? configureServices)
    {
        if (_withMockedMediator)
            configureServices += s =>
            {
                if (IsCqrsComplete()) s.AddSingleton(_customMediatorMock);
                else s.AddSingleton(_mediatRMock);
            };
        else
        {
            if (IsCqrsComplete())
                configureServices += s => s.AddMediator(Features.Helpers.AssemblyReference.Assembly);
            else
                configureServices += s => s.AddMediatR(cfg =>
                    cfg.RegisterServicesFromAssembly(Features.Helpers.AssemblyReference.Assembly));
        }
    }

    private void SetupPipelineBehavior(ref Action<IServiceCollection>? configureServices)
    {
        if (_withPipelineBehaviors) // Порядок pipelineBehaviors может быть нарушен
        {
            var types = LoadPipelineBehavior();
            foreach (var type in types)
            {
                configureServices += s => s
                    .AddSingleton(typeof(Mediator.IPipelineBehavior<,>), type);
            }
        }
    }

    private void GetMockedMediatorFromServiceProvider()
    {
        if (!_withMockedMediator)
        {
            if (IsCqrsComplete())
                _customMediatorMock = ServiceProvider!.GetRequiredService<Mediator.IMediator>();
            else
                _mediatRMock = ServiceProvider!.GetRequiredService<MediatR.IMediator>();
        }
    }
}