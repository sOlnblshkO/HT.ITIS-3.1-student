using Dotnet.Homeworks.Shared.MessagingContracts.Email;

namespace Dotnet.Homeworks.MainProject.Services;

public class CommunicationService : ICommunicationService
{
    public Task SendEmailAsync(SendEmail sendEmailDto)
    {
        // TODO: implement RabbitMq messaging
        throw new NotImplementedException();
    }
}