namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;


public class ImplementInterfacesException : Exception
{
    public ImplementInterfacesException(string assembly)
        : base(FormatMessage(assembly)) { }

    private static string FormatMessage(string assembly) => $"Not all class implement certain interface in {assembly} assembly";
}