using System.Reflection;
using System.Security.Claims;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Infrastructure.Validation.PermissionChecker.DependencyInjectionExtensions;
using Dotnet.Homeworks.Mediator;
using Dotnet.Homeworks.Mediator.DependencyInjectionExtensions;
using Dotnet.Homeworks.Tests.Cqrs.Helpers;
using Dotnet.Homeworks.Tests.CqrsValidation.Helpers;
using Dotnet.Homeworks.Tests.RunLogic.Utils.TestEnvironmentBuilder;
using Dotnet.Homeworks.Tests.Shared.CqrsStuff;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Dotnet.Homeworks.Tests.MongoDb.Helpers;

public class MongoEnvironmentBuilder : TestEnvironmentBuilder<MongoEnvironment>
{
    private static readonly Assembly FeaturesAssembly = Features.Helpers.AssemblyReference.Assembly;
    
    private IMediator? _mediator;
    private IHttpContextAccessor? _contextAccessor;
    private Guid? _contextUserId;

    private bool _withFakeUser;

    /// <summary>
    /// Sets new Claim of NameIdentifier to the User of the current HttpContext
    /// </summary>
    /// <remarks>User, encapsulated in the Context by this method, does not exist in UsersRepository. It doesn't exist for the app.</remarks>
    public MongoEnvironmentBuilder WithFakeUserInContext(bool withFakeUser = true)
    {
        _withFakeUser = withFakeUser;
        ConfigureContextUser();
        return this;
    }
    
    public override void SetupServices(Action<IServiceCollection>? configureServices = default)
    {
        configureServices += s => s
            .AddSingleton<IOrderRepository, OrderRepositoryMock>()
            .AddSingleton<IProductRepository, ProductRepositoryMock>()
            .AddSingleton(Substitute.For<IUnitOfWork>())
            .AddSingleton<IUserRepository, UserRepositoryMock>()
            .AddMediator(FeaturesAssembly)
            .AddSingleton(_contextAccessor ?? InitializeContextAccessor());
        configureServices += s => s.AddValidatorsFromAssembly(FeaturesAssembly);
        configureServices += s => s.AddPermissionChecks(FeaturesAssembly);
        configureServices += SetupPipelineBehavior;
        ServiceProvider = GetServiceProvider(configureServices);
    }

    public override MongoEnvironment Build()
    {
        if (ServiceProvider is null) SetupServices();
        _mediator ??= ServiceProvider!.GetRequiredService<IMediator>();
        _contextAccessor ??= ServiceProvider!.GetRequiredService<IHttpContextAccessor>();
        return new MongoEnvironment(_mediator, _contextAccessor, _contextUserId);
    }

    private IHttpContextAccessor InitializeContextAccessor()
    {
        _contextAccessor = new HttpContextAccessor();
        _contextAccessor.HttpContext = new DefaultHttpContext();
        return _contextAccessor;
    }

    private void ConfigureContextUser()
    {
        if (!_withFakeUser) return;
        _contextUserId = Guid.NewGuid();
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, _contextUserId.Value.ToString()) };
        var claimsIdentity = new ClaimsIdentity(claims);
        if (_contextAccessor is null) InitializeContextAccessor();
        _contextAccessor!.HttpContext!.User = new ClaimsPrincipal(claimsIdentity);
    }

    private static void SetupPipelineBehavior(IServiceCollection services)
    {
        var types = LoadPipelineBehavior();
        foreach (var type in types)
            services.AddSingleton(typeof(IPipelineBehavior<,>), type);
    }

    private static IEnumerable<Type> LoadPipelineBehavior()
    {
        var pipelineBehaviors = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(x => x.GetInterfaces().Any(t =>
                t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>)));

        return pipelineBehaviors;
    }
}