using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mailing.API.Dto;

namespace Dotnet.Homeworks.Mailing.API.Services;

public interface IMailingService
{
    public Task<Result> SendEmailAsync(EmailMessage emailDto);
}