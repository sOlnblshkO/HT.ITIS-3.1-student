using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Dotnet.Homeworks.Tests.Shared.Docker;

public static class Parser
{
    private const string FilePath = "docker-compose.yml";

    private static readonly Lazy<DockerCompose> DockerComposeDeserializedFactory = new(() =>
    {
        var filePath = Path.Combine(TryGetSolutionDirectoryInfo().FullName, FilePath);

        if (!File.Exists(filePath)) throw new InvalidOperationException();

        var b = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .WithNamingConvention(UnderscoredNamingConvention.Instance);

        var yamlContent = File.ReadAllText(filePath);
        
        return b.Build().Deserialize<DockerCompose>(yamlContent);
    });

    private static readonly DockerCompose DockerComposeDeserialized = DockerComposeDeserializedFactory.Value;

    private static DirectoryInfo TryGetSolutionDirectoryInfo()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (directory is not null && !directory.GetFiles("*.sln").Any())
            directory = directory.Parent;

        if (directory is null) throw new DirectoryNotFoundException("Solution directory not found");

        return directory;
    }

    public static DockerCompose Parse() => DockerComposeDeserialized;
}