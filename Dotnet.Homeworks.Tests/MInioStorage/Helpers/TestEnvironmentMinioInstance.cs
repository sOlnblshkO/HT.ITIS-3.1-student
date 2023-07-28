using Dotnet.Homeworks.Storage.API.Configuration;

namespace Dotnet.Homeworks.Tests.MinioStorage.Helpers;

public static class TestEnvironmentMinioInstance
{
    public static readonly MinioConfig Config = new()
    {
        Username = "m1n10adm1n",
        Password = "m1n10adm1n",
        Endpoint = "localhost",
        Port = 9002,
        WithSsl = false
    };
}