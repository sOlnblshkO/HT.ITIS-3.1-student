using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using MassTransit;

namespace Dotnet.Homeworks.MainProject.Services;

public class CommunicationService : ICommunicationService
{
    private readonly IPublishEndpoint _publish;

    public CommunicationService(IPublishEndpoint publish)
    {
        _publish = publish;
    }

    public async Task SendEmailAsync(SendEmail sendEmailDto, CancellationToken cancellationToken)
        =>  await _publish.Publish(
            new SendEmail(
                sendEmailDto.ReceiverName,
                sendEmailDto.ReceiverEmail,
                sendEmailDto.Subject,
                sendEmailDto.Content),
            cancellationToken);
}