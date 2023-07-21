namespace Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

public class DockerCompose
{
    public ServicesSection? Services { get; set; }
}

public class ServicesSection
{
    public DotnetPostgres? DotnetPostgres { get; set; }

    public DotnetRabbitmq? DotnetRabbitmq { get; set; }
    
    public DotnetMain? DotnetMain { get; set; }

    public DotnetMailing? DotnetMailing { get; set; }
}

public class DotnetPostgres : HasEnvironment
{
}

public class DotnetRabbitmq : HasEnvironment
{
}

public class DotnetMain : DotnetProject
{
}

public class DotnetMailing : DotnetProject
{
}