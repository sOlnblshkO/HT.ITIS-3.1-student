using Dotnet.Homeworks.Mailing.API.Dto;

namespace Dotnet.Homeworks.Mailing.API.Services;

public interface IMailingService
{
    public Task<BaseResult> SendEmail(EmailMessage emailDto);
}