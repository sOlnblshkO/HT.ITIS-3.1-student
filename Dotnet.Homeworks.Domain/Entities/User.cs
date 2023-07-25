namespace Dotnet.Homeworks.Domain.Entities;

/// <summary>
/// User model
/// <param name="Name"></param>
/// <param name="Email"></param>
/// </summary>
public class User : BaseEntity
{
    public string Email { get; set; }

    public string Name { get; set; }
}