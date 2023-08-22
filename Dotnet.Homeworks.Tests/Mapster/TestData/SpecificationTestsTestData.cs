using System.Linq.Expressions;
using Dotnet.Homeworks.DataAccess.Specs;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.Tests.Mapster;

public partial class SpecificationTests
{
    public static IEnumerable<object?[]> RequiredMethodsTestData()
    {
        // should have implicit conversion to Expression<Func<T, bool>>
        yield return new object?[]
        {
            typeof(Expression<>).MakeGenericType(
                typeof(Func<,>).MakeGenericType(DestinationType, typeof(bool))),
            "op_Implicit",
            new[] { SpecificationType }
        };
        // should have || operator override
        yield return new object?[] { SpecificationType, "op_BitwiseOr", null };
        // should have && operator override
        yield return new object?[] { SpecificationType, "op_BitwiseAnd", null };
    }

    public static IEnumerable<object[]> FilterTestData()
    {
        const string googleEmail = "test.test@gmail.com";
        const string yandexEmail = "test.test@yandex.ru";
        const string mailEmail = "test.test@mail.ru";
        const string shortName = "short";
        const string longName = "loooooooooooooooooooooong";
        const string compositeShortName = $"{shortName}-{shortName}";
        const string complexLongName = $"{shortName} {longName}";

        var users = new User[]
        {
            new() { Email = googleEmail, Name = shortName },            // 0
            new() { Email = yandexEmail, Name = shortName },            // 1
            new() { Email = mailEmail, Name = shortName },              // 2
            new() { Email = googleEmail, Name = longName },             // 3
            new() { Email = googleEmail, Name = compositeShortName },   // 4
            new() { Email = googleEmail, Name = complexLongName },      // 5
            new() { Email = "test.test@test.tst", Name = shortName }    // 6
        };
        var specs = new UsersSpecs();
        var t = specs.HasYandexEmail;

        yield return new object[]
            { specs.HasGoogleEmail, users, new[] { users[0], users[3], users[4], users[5] } };
        yield return new object[] { specs.HasYandexEmail, users, new[] { users[1] } };
        yield return new object[] { specs.HasMailEmail, users, new[] { users[2] } };
        yield return new object[] { specs.HasPopularEmailVendor, users, users[..^1] };
        yield return new object[] { specs.HasLongName, users, new[] { users[3], users[5] } };
        yield return new object[] { specs.HasCompositeName, users, new[] { users[4], users[5] } };
        yield return new object[] { specs.HasComplexName, users, new[] { users[5] } };
    }
}