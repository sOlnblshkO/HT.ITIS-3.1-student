namespace Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;

public class DeleteProductByGuidCommand //TODO: Inherit certain interface 
{
    public Guid Guid { get; init; }

    public DeleteProductByGuidCommand(Guid guid)
    {
        Guid = guid;
    }
}