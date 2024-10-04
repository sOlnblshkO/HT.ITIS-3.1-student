using Dotnet.Homeworks.Mailing.API.Dto;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using MassTransit;

namespace Dotnet.Homeworks.Mailing.API.Consumers;

public class EmailConsumer : IEmailConsumer
{
    private readonly IMailingService _mailingService;

    public EmailConsumer(IMailingService mailingService)
    {
        _mailingService = mailingService;
    }

    public async Task Consume(ConsumeContext<SendEmail> context)
    {
        context.CancellationToken.ThrowIfCancellationRequested();
        var message = context.Message;

        var emailMessage = new EmailMessage(
            message.ReceiverEmail,
            message.Subject,
            message.Content);

        await _mailingService.SendEmailAsync(emailMessage);
    }
}