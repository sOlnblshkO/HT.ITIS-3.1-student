namespace Dotnet.Homeworks.MessagingContracts.Email;

public record SendEmail(string ReceiverName, string ReceiverEmail, string Subject, string Content);