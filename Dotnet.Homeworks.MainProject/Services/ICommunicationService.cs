using Dotnet.Homeworks.MessagingContracts.Email;

namespace Dotnet.Homeworks.MainProject.Services;

public interface ICommunicationService
{
    public Task SendEmailAsync(SendEmail sendEmailDto);
}