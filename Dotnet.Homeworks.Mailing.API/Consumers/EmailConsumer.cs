using Dotnet.Homeworks.MessagingContracts.Email;
using MassTransit;

namespace Dotnet.Homeworks.Mailing.API.Consumers;

public class EmailConsumer : IEmailConsumer
{
    public Task Consume(ConsumeContext<SendEmail> context)
    {
        // TODO: implement
        throw new NotImplementedException();
    }
}