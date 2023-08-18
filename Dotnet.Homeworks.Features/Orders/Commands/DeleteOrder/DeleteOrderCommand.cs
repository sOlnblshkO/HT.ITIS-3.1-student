namespace Dotnet.Homeworks.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderByGuidCommand // TODO: implement interface
{
    public DeleteOrderByGuidCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; init; }
}