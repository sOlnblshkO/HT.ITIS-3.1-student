namespace Dotnet.Homeworks.Tests.MinioStorage.Helpers;

public class PortAlreadyAllocatedException : Exception
{
    public PortAlreadyAllocatedException(int port) : base(FormatMessage(port))
    { }

    private static string FormatMessage(int port) => $"The {port} port is already allocated on your system";
}