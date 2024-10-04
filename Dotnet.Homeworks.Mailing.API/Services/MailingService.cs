using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Dto;
using Dotnet.Homeworks.Shared.Dto;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Dotnet.Homeworks.Mailing.API.Services;

public class MailingService : IMailingService
{
    private readonly EmailConfig _emailConfig;

    public MailingService(IOptions<EmailConfig> emailConfig)
    {
        _emailConfig = emailConfig.Value;
    }

    public async Task<Result> SendEmailAsync(EmailMessage emailDto)
    {
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Testing mailing api", _emailConfig.Email));
        message.To.Add(new MailboxAddress(emailDto.Email, emailDto.Email));
        message.Subject = emailDto.Subject ?? "";
        var bodyBuilder = new BodyBuilder
        {
            TextBody = $"Your message: {emailDto.Content}"
        };
        message.Body = bodyBuilder.ToMessageBody();
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailConfig.Host, _emailConfig.Port);
            await client.AuthenticateAsync(_emailConfig.Email, _emailConfig.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return new Result(true);
        }
        catch (Exception ex)
        {
            return new Result(false, $"Error while sending email: {ex.Message}");
        }
    }
}