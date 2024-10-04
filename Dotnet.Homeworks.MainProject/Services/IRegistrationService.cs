using Dotnet.Homeworks.MainProject.Dto;

namespace Dotnet.Homeworks.MainProject.Services;

public interface IRegistrationService
{
    public Task RegisterAsync(RegisterUserDto userDto, CancellationToken cancellationToken);
}