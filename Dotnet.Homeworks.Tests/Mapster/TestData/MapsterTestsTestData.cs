using Dotnet.Homeworks.Features.Orders.Mapping;
using Dotnet.Homeworks.Features.Cqrs.Products.Mapping;
using Dotnet.Homeworks.Features.Cqrs.UserManagement.Mapping;
using Dotnet.Homeworks.Features.Cqrs.Users.Mapping;

namespace Dotnet.Homeworks.Tests.Mapster;

public partial class MapsterTests
{
    public static IEnumerable<object[]> EnumerateIMappers()
    {
        yield return new object[] { typeof(IOrderMapper) };
        yield return new object[] { typeof(IProductMapper) };
        yield return new object[] { typeof(IUserMapper) };
        yield return new object[] { typeof(IUserManagementMapper) };
    }

    public static IEnumerable<object[]> EnumerateRegisterMappings()
    {
        yield return new object[] { typeof(RegisterOrderMappings) };
        yield return new object[] { typeof(RegisterProductMappings) };
        yield return new object[] { typeof(RegisterUserMappings) };
        yield return new object[] { typeof(RegisterUserManagementMappings) };
    }
}