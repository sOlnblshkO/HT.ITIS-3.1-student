using Dotnet.Homeworks.Shared.MessagingContracts.Email;

namespace Dotnet.Homeworks.MainProject.Services;

public interface ICommunicationService
{
    public Task SendEmailAsync(SendEmail sendEmailDto, CancellationToken cancellationToken);
}