using Dotnet.Homeworks.Storage.API.Configuration;

namespace Dotnet.Homeworks.Tests.MinioStorage.Helpers;

public static class TestEnvironmentMinioInstance
{
    public static readonly MinioConfig Config = new()
    {
        Username = "testMinioAdmin",
        Password = "t35tM1n10A4m1nP655w0r4",
        Endpoint = "localhost",
        Port = 9002,
        WithSsl = false
    };
}