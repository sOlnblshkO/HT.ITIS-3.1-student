namespace Dotnet.Homeworks.Shared.MessagingContracts.Email;

public record SendEmail(string ReceiverName, string ReceiverEmail, string Subject, string Content);