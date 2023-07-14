namespace Dotnet.Homeworks.Mailing.API.Dto;

public class EmailMessage
{
    public string Email { get; set; }
    public string? Subject { get; set; }
    public string Content { get; set; }
}