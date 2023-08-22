using System.Reflection;
using Dotnet.Homeworks.Features.Cqrs.Products.Mapping;
using Dotnet.Homeworks.Features.Cqrs.UserManagement.Mapping;
using Dotnet.Homeworks.Features.Cqrs.Users.Mapping;
using Dotnet.Homeworks.Features.Helpers;
using Dotnet.Homeworks.Features.Orders.Mapping;
using Dotnet.Homeworks.MainProject.ServicesExtensions.Mapper;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using NetArchTest.Rules;
using ServiceCollectionExtensions = Dotnet.Homeworks.MainProject.ServicesExtensions.Mapper.ServiceCollectionExtensions;

namespace Dotnet.Homeworks.Tests.Mapster;

public partial class MapsterTests
{
    private static readonly Assembly FeaturesAssembly = AssemblyReference.Assembly;

    [Homework(RunLogic.Homeworks.AutoMapper)]
    public void AddMappersExtension_ShouldBe_Implemented()
    {
        var services = new ServiceCollection();
        try
        {
            services.AddMappers(FeaturesAssembly);
        }
        catch (NotImplementedException)
        {
            Assert.Fail($"{nameof(ServiceCollectionExtensions.AddMappers)} extension method is not implemented");
        }
    }

    [HomeworkTheory(RunLogic.Homeworks.AutoMapper)]
    [MemberData(nameof(EnumerateIMappers))]
    public void MappersInterfaces_ShouldNotBe_Empty(Type mapperType)
    {
        var methods = mapperType.GetMethods();

        Assert.NotEmpty(methods);
    }

    [HomeworkTheory(RunLogic.Homeworks.AutoMapper)]
    [MemberData(nameof(EnumerateIMappers))]
    public void Mappers_ShouldBe_Generated(Type mapperInterface)
    {
        var featuresNamespace = FeaturesAssembly.GetName().Name;
        var mappersTypes = Types
            .InAssembly(FeaturesAssembly)
            .That()
            .ResideInNamespace(featuresNamespace)
            .GetTypes()
            .Where(mapperType => mapperType.GetInterface(mapperInterface.Name) is not null);

        Assert.NotEmpty(mappersTypes);
    }

    [Homework(RunLogic.Homeworks.AutoMapper)]
    public void AtLeastOneMapper_ShouldMap_FromIQueryable()
    {
        var mappers = new[]
            { typeof(IOrderMapper), typeof(IProductMapper), typeof(IUserMapper), typeof(IUserManagementMapper) };
        foreach (var mapperInterface in mappers)
        {
            var methods = mapperInterface.GetMethods();

            var foundIQueryable = methods
                .Any(m => m.GetParameters()
                    .Any(p => p.ParameterType.IsGenericType &&
                              p.ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>)));
            if (foundIQueryable) return; // test is passed
        }
        Assert.Fail("No mapper has a method with IQueryable as one of the parameters types");
    }

    [HomeworkTheory(RunLogic.Homeworks.AutoMapper)]
    [MemberData(nameof(EnumerateRegisterMappings))]
    public void RegisterMappings_ShouldImplement_IRegisterInterface(Type registerOrderMapping)
    {
        var registerInterface = registerOrderMapping.GetInterface(nameof(IRegister));

        Assert.NotNull(registerInterface);
    }

    [HomeworkTheory(RunLogic.Homeworks.AutoMapper)]
    [MemberData(nameof(EnumerateRegisterMappings))]
    public void RegisterMappings_ShouldHave_Instructions_InRegisterMethod(Type registerOrderMapping)
    {
        var methodInfo = registerOrderMapping.GetMethod(nameof(IRegister.Register));

        Assert.True(HasInstructions(methodInfo));
    }

    private static bool HasInstructions(MethodInfo? methodInfo)
    {
        var methodBody = methodInfo?.GetMethodBody();
        var methodCode = methodBody?.GetILAsByteArray();

        return methodCode is not null && methodCode.Length > 0;
    }
}