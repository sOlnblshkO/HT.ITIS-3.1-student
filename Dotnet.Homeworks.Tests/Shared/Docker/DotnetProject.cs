﻿namespace Dotnet.Homeworks.Tests.Shared.Docker;

public abstract class DotnetProject : HasEnvironment
{
    public Dictionary<string, string>? Build { get; set; }

    public List<string>? DependsOn { get; set; }
}