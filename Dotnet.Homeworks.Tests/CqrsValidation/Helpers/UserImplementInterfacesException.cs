namespace Dotnet.Homeworks.Tests.CqrsValidation.Helpers;


public class UserImplementInterfacesException : Exception
{
    public UserImplementInterfacesException(string assembly)
        : base(FormatMessage(assembly)) { }

    private static string FormatMessage(string assembly) => $"Not all Product's feature classes implement certain interface in {assembly} assembly";
}