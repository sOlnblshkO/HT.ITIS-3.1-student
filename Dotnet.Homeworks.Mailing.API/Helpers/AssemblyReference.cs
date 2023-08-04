using System.Reflection;

namespace Dotnet.Homeworks.Mailing.API.Helpers;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}