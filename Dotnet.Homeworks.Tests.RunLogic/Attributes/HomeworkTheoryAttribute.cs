using Xunit;

namespace Dotnet.Homeworks.Tests.RunLogic.Attributes;

public class HomeworkTheoryAttribute: TheoryAttribute
{
    private readonly Homeworks _number;
    private readonly bool _skipIfPassed;

    public HomeworkTheoryAttribute(Homeworks number, bool skipIfPassed = default)
    {
        _number = number;
        _skipIfPassed = skipIfPassed;
    }

    public override string? Skip => HomeworkAttribute.GetSkip(_number, _skipIfPassed);
}