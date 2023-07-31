using Dotnet.Homeworks.MainProject.Dto;
using Dotnet.Homeworks.Shared.MessagingContracts.Email;

namespace Dotnet.Homeworks.MainProject.Services;

public class RegistrationService : IRegistrationService
{
    private readonly ICommunicationService _communicationService;

    public RegistrationService(ICommunicationService communicationService)
    {
        _communicationService = communicationService;
    }

    public async Task RegisterAsync(RegisterUserDto userDto)
    {
        // pretending we have some complex logic here
        await Task.Delay(100);
        
        // publish message to a queue
        await _communicationService.SendEmailAsync(new SendEmail("", "", "", ""));
    }
}