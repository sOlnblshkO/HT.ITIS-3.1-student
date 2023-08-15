using System.Reflection;

namespace Dotnet.Homeworks.Mediator.Helpers;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}