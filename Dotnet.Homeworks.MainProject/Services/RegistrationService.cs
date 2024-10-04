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

    public async Task RegisterAsync(RegisterUserDto userDto, CancellationToken cancellationToken)
    {
        // pretending we have some complex logic here
        await Task.Delay(100, cancellationToken);
        
        // publish message to a queue
        await _communicationService.SendEmailAsync(
            new SendEmail(
                "Register",
                userDto.Email,
                "Registration",
                "You are registration"),
            cancellationToken);
    }
}