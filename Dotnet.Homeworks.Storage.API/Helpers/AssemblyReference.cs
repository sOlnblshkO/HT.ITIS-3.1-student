using System.Reflection;

namespace Dotnet.Homeworks.Storage.API.Helpers;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}