using System.Reflection;
using System.Security.Claims;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Features.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Products.Queries.GetProducts;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Infrastructure.Validation.PermissionChecker.DependencyInjectionExtensions;
using Dotnet.Homeworks.MainProject.Controllers;
using Dotnet.Homeworks.Mediator.DependencyInjectionExtensions;
using Dotnet.Homeworks.Shared.Dto;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Dotnet.Homeworks.Tests.Shared.RepositoriesMocks;
using Dotnet.Homeworks.Tests.Shared.TestEnvironmentBuilder;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;

internal class CqrsEnvironmentBuilder : TestEnvironmentBuilder<CqrsEnvironment>
{
    private ProductRepositoryMock ProductRepositoryMock { get; } = Substitute.For<ProductRepositoryMock>();
    private UserRepositoryMock UserRepositoryMock { get; } = Substitute.For<UserRepositoryMock>();
    private IHttpContextAccessor HttpContextAccessorMock { get; } = Substitute.For<IHttpContextAccessor>();

    private IUnitOfWork UnitOfWork { get; set; } = Substitute.For<IUnitOfWork>();
    private MediatR.IMediator MediatR { get; set; } = Substitute.For<MediatR.IMediator>();
    private Mediator.IMediator CustomMediator { get; set; } = Substitute.For<Mediator.IMediator>();
    private ProductManagementController? ProductManagementController { get; set; }

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
            .AddSingleton(UnitOfWork)
            .AddValidatorsFromAssembly(Features.Helpers.AssemblyReference.Assembly)
            .AddPermissionChecks(Features.Helpers.AssemblyReference.Assembly);

        configureServices = SetupMediator(configureServices);

        configureServices = SetupPipelineBehavior(configureServices);

        ServiceProvider = GetServiceProvider(configureServices);
    }

    public override CqrsEnvironment Build()
    {
        if (ServiceProvider is null) SetupServices();
        ProductManagementController ??= ServiceProvider!.GetRequiredService<ProductManagementController>();
        GetMockedMediatorFromServiceProvider();

        return new CqrsEnvironment(ProductManagementController,
            UnitOfWork, MediatR, CustomMediator, UserRepositoryMock);
    }

    public void SetupHttpContextClaims(List<Claim> claims)
    {
        var httpContextSubstitute = Substitute.For<HttpContext>();
        httpContextSubstitute.User.Claims.Returns(claims);

        HttpContextAccessorMock.HttpContext.Returns(httpContextSubstitute);
    }

    private IEnumerable<Type> LoadPipelineBehavior()
    {
        var pipelineBehaviors = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(type => type.GetInterfaces().Any(interfaceType =>
                interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition() == typeof(Mediator.IPipelineBehavior<,>)));

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
        MediatR.Send(Arg.Any<ICommand>(), Arg.Any<CancellationToken>())
            .Returns(new Result(true));
        MediatR.Send(Arg.Any<ICommand<InsertProductDto>>(), Arg.Any<CancellationToken>())
            .Returns(new Result<InsertProductDto>(new InsertProductDto(Guid.Empty), true));
        MediatR.Send(Arg.Any<IQuery<GetProductsDto>>(), Arg.Any<CancellationToken>())
            .Returns(new Result<GetProductsDto>(
                new GetProductsDto(new List<GetProductDto>() { new GetProductDto(Guid.NewGuid(), "name") }), true));
    }

    private void SetupCustomMediatorMock()
    {
        CustomMediator.Send(Arg.Any<ICommand>(), Arg.Any<CancellationToken>())
            .Returns(new Result(true));
        CustomMediator.Send(Arg.Any<ICommand<InsertProductDto>>(), Arg.Any<CancellationToken>())
            .Returns(new Result<InsertProductDto>(new InsertProductDto(Guid.Empty), true));
        CustomMediator.Send(Arg.Any<IQuery<GetProductsDto>>(), Arg.Any<CancellationToken>())
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

    private Action<IServiceCollection> SetupMediator(Action<IServiceCollection>? configureServices)
    {
        if (_withMockedMediator)
            configureServices += s =>
            {
                if (IsCqrsComplete()) s.AddSingleton(CustomMediator);
                else s.AddSingleton(MediatR);
            };
        else
        {
            if (IsCqrsComplete())
                configureServices += s => s.AddMediator(Features.Helpers.AssemblyReference.Assembly);
            else
                configureServices += s => s.AddMediatR(cfg =>
                    cfg.RegisterServicesFromAssembly(Features.Helpers.AssemblyReference.Assembly));
        }

        return configureServices;
    }

    private Action<IServiceCollection>? SetupPipelineBehavior(Action<IServiceCollection>? configureServices)
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

        return configureServices;
    }

    private void GetMockedMediatorFromServiceProvider()
    {
        if (!_withMockedMediator)
        {
            if (IsCqrsComplete())
                CustomMediator = ServiceProvider!.GetRequiredService<Mediator.IMediator>();
            else
                MediatR = ServiceProvider!.GetRequiredService<MediatR.IMediator>();
        }
    }
}