using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Dto;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/email", async (IOptions<EmailConfig> emailOptions, EmailMessage emailDto) =>
{
    var emailConfig = emailOptions.Value;
    using var message = new MimeMessage();
    message.From.Add(new MailboxAddress("Testing mailing api", emailConfig.Email));
    message.To.Add(new MailboxAddress("", emailDto.Email));
    message.Subject = emailDto.Subject ?? "";
    var bodyBuilder = new BodyBuilder
    {
        TextBody = $"Your message: {emailDto.Content}"
    };
    message.Body = bodyBuilder.ToMessageBody();
    using var client = new SmtpClient();
    try
    {
        await client.ConnectAsync(emailConfig.Host, emailConfig.Port, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(emailConfig.Email, emailConfig.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
        return Results.Ok();
    }
    catch (Exception)
    {
        return Results.BadRequest("Error while sending email");
    }
});

app.Run();