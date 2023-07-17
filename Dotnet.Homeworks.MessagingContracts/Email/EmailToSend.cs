namespace Dotnet.Homeworks.MessagingContracts.Email;

public record EmailToSend(string ReceiverName, string ReceiverEmail, string? Subject, string Content);