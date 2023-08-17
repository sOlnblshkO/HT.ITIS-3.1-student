namespace Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;

public class NoConsumersInAssemblyException : Exception
{
    public NoConsumersInAssemblyException(string assembly)
        : base(FormatMessage(assembly))
    {
    }

    private static string FormatMessage(string assembly) => $"There is no consumers in {assembly} assembly";
}