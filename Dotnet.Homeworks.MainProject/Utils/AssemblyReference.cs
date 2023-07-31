using System.Reflection;

namespace Dotnet.Homeworks.MainProject.Utils;

public class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}