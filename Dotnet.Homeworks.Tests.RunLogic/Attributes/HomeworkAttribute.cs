using System.Reflection;
using Xunit;

namespace Dotnet.Homeworks.Tests.RunLogic.Attributes;

public class HomeworkAttribute : FactAttribute
{
    private readonly Homeworks _number;
    private readonly bool _skipIfPassed;

    public HomeworkAttribute(Homeworks number, bool skipIfPassed = default)
    {
        _number = number;
        _skipIfPassed = skipIfPassed;
    }

    public override string? Skip => GetSkip(_number, _skipIfPassed);

    internal static string? GetSkip(Homeworks number, bool skipIfPassed)
    {
        var hw = typeof(HomeworkProgressAttribute)
            .Assembly
            .GetCustomAttribute<HomeworkProgressAttribute>()?.Number;

        if (hw == null)
        {
            throw new InvalidOperationException("Test assembly must be marked with HomeworkProgressAttribute");
        }

        var assemblyNumber = (byte)hw.Value;
        return (byte)number <= assemblyNumber && ( !skipIfPassed || (byte)number == assemblyNumber )
            ? null
            : "Next Homeworks";
    }
}