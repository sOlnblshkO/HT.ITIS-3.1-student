using System.Reflection;
using Xunit;

namespace Dotnet.Homeworks.Tests.RunLogic.Attributes;

public class HomeworkAttribute : FactAttribute
{
    private readonly Homeworks _number;
    private readonly bool _skipIfNotCurrentHomework;

    public HomeworkAttribute(Homeworks number, bool skipIfNotCurrentHomework = default)
    {
        _number = number;
        _skipIfNotCurrentHomework = skipIfNotCurrentHomework;
    }

    public override string? Skip => GetSkip(_number, _skipIfNotCurrentHomework);

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
        return (byte)number <= assemblyNumber && (!skipIfPassed || (byte)number == assemblyNumber)
            ? null
            : "Next Homeworks";
    }
}