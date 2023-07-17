namespace Dotnet.Homeworks.Mailing.API.Dto;

public record EmailMessage(string Email, string? Subject, string Content);