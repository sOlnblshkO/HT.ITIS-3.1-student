using System.Reflection;

namespace Dotnet.Homeworks.MainProject.Helpers;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}