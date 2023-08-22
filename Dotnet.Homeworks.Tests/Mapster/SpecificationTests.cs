using System.Reflection;
using Dotnet.Homeworks.DataAccess.Specs.Infrastructure;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Tests.RunLogic.Attributes;

namespace Dotnet.Homeworks.Tests.Mapster;

public partial class SpecificationTests
{
    private static readonly Type DestinationType = typeof(User);
    private static readonly Type SpecificationType = typeof(Specification<>).MakeGenericType(DestinationType);

    private static MethodInfo? GetMethod(Type sourceType, string testMethod, Type[]? typesArgs)
    {
        return typesArgs is null
            ? sourceType.GetMethod(testMethod)
            : sourceType.GetMethod(testMethod, typesArgs);
    }

    // Cannot use InlineData because it cannot receive non-const arguments
    [HomeworkTheory(RunLogic.Homeworks.AutoMapper)]
    [MemberData(nameof(RequiredMethodsTestData))]
    public void ShouldHave_RequiredMethods(Type expectedReturnType, string testMethod, Type[] typesArgs)
    {
        var requiredMethod = GetMethod(SpecificationType, testMethod, typesArgs);

        Assert.NotNull(requiredMethod);
        Assert.Equal(expectedReturnType, requiredMethod.ReturnType);
    }

    [HomeworkTheory(RunLogic.Homeworks.AutoMapper)]
    [MemberData(nameof(FilterTestData))]
    public void ShouldFilter_Correctly(Func<IQueryableFilter<User>> specificationMethod, IEnumerable<User> input,
        IEnumerable<User> expectedOutput)
    {
        var actualOutput = specificationMethod().Apply(input.AsQueryable());

        Assert.True(actualOutput.SequenceEqual(expectedOutput));
    }
}