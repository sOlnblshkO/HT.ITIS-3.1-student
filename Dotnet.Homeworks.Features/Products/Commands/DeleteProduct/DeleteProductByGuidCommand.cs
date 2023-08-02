namespace Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;

public class DeleteProductByGuidCommand
{
    public Guid Guid { get; set; }

    public DeleteProductByGuidCommand(Guid guid)
    {
        Guid = guid;
    }
}