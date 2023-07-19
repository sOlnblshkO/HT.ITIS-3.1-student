using Dotnet.Homeworks.MessagingContracts.Email;
using MassTransit;

namespace Dotnet.Homeworks.Mailing.API.Consumers;

public interface IEmailConsumer : IConsumer<SendEmail>
{ }