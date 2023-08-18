using System.Reflection;

namespace Dotnet.Homeworks.DataAccess.Helpers;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}