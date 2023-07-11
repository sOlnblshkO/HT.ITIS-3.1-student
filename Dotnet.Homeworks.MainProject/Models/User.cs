using System.ComponentModel.DataAnnotations;

namespace Dotnet.Homeworks.MainProject.Models;

public class User : BaseEntity
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}