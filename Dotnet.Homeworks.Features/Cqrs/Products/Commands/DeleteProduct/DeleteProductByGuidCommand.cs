namespace Dotnet.Homeworks.Features.Cqrs.Products.Commands.DeleteProduct;

public class DeleteProductByGuidCommand
{
    public Guid Guid { get; init; }

    public DeleteProductByGuidCommand(Guid guid)
    {
        Guid = guid;
    }
}