using Dotnet.Homeworks.Mailing.API.Dto;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Mailing.API.Services;

public interface IMailingService
{
    public Task<BaseResult> SendEmailAsync(EmailMessage emailDto);
}