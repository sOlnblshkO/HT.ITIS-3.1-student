namespace Dotnet.Homeworks.Tests.Docker;

public class DockerCompose
{
    public ServicesSection? Services { get; set; }
}

public class ServicesSection
{
    public DotnetWeb? DotnetWeb { get; set; }
    
    public DotnetPostgres? DotnetPostgres { get; set; }
}

public class DotnetWeb
{
    public Dictionary<string, string>? Environment { get; set; }
    
    public Dictionary<string, string>? Build { get; set; }
    
    public List<string>? DependsOn { get; set; }
}
public class DotnetPostgres
{
    public Dictionary<string, string>? Environment { get; set; }
}