namespace Dotnet.Homeworks.Domain.Entities;


public class User : BaseEntity
{
    /// <summary>
    /// Почта пользователя
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
}
