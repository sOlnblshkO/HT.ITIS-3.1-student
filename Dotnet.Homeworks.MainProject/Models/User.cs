namespace Dotnet.Homeworks.MainProject.Models;

public class User : BaseEntity
{
    /// <summary>
    /// Email пользователя
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
}