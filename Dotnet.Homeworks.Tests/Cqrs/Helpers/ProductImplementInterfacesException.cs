namespace Dotnet.Homeworks.Tests.Cqrs.Helpers;


public class ProductImplementInterfacesException : Exception
{
    public ProductImplementInterfacesException(string assembly)
        : base(FormatMessage(assembly)) { }

    private static string FormatMessage(string assembly) => $"Not User's feature classes implement certain interface in {assembly} assembly";
}