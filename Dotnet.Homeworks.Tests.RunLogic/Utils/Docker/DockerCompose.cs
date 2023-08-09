namespace Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

public class DockerCompose
{
    public ServicesSection? Services { get; set; }
}

public class ServicesSection
{
    public DotnetPostgres? DotnetPostgres { get; set; }

    public DotnetRabbitmq? DotnetRabbitmq { get; set; }

    public DotnetMinio? DotnetMinio { get; set; }

    public DotnetJaeger? DotnetJaeger { get; set; }

    public DotnetMain? DotnetMain { get; set; }

    public DotnetMailing? DotnetMailing { get; set; }

    public DotnetStorage? DotnetStorage { get; set; }
}

public class DotnetPostgres : HasEnvironment
{
}

public class DotnetRabbitmq : HasEnvironment
{
}

public class DotnetMinio : HasEnvironment
{
}

public class DotnetJaeger : HasEnvironment
{
}

public class DotnetMain : DotnetProject
{
}

public class DotnetMailing : DotnetProject
{
}

public class DotnetStorage : DotnetProject
{
}