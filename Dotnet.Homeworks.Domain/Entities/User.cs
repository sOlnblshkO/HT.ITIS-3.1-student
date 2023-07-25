namespace Dotnet.Homeworks.Domain.Entities;


public class User : BaseEntity
{
    /// <summary>
    /// Почта
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
}