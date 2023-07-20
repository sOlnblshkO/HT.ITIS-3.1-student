namespace Dotnet.Homeworks.Tests.MasstransitRabbit.Helpers;

public class NoConsumersInAssemblyException : Exception
{
    public NoConsumersInAssemblyException(string assembly)
        : base(FormatMessage(assembly)) { }

    public NoConsumersInAssemblyException(string assembly, Exception innerException)
        : base(FormatMessage(assembly), innerException) { }

    private static string FormatMessage(string assembly) => $"There is no found consumers in {assembly} assembly";
}