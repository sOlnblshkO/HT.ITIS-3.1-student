namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;


public class ProductImplementInterfacesException : Exception
{
    public ProductImplementInterfacesException(string assembly)
        : base(FormatMessage(assembly)) { }

    private static string FormatMessage(string assembly) => $"Not all Products feature types implement required interfaces in {assembly} assembly";
}