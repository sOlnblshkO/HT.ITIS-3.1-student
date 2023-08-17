using Xunit;

namespace Dotnet.Homeworks.Tests.RunLogic.Attributes;

public class HomeworkTheoryAttribute : TheoryAttribute
{
    private readonly Homeworks _number;
    private readonly bool _skipIfNotCurrentHomework;

    public HomeworkTheoryAttribute(Homeworks number, bool skipIfNotCurrentHomework = default)
    {
        _number = number;
        _skipIfNotCurrentHomework = skipIfNotCurrentHomework;
    }

    public override string? Skip => HomeworkAttribute.GetSkip(_number, _skipIfNotCurrentHomework);
}