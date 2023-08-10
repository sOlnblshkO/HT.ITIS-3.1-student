namespace Dotnet.Homeworks.Features.Cqrs.Products.Commands.UpdateProduct;

public class UpdateProductCommand
{
    public Guid Guid { get; init; }
    public string Name { get; init; }
    
    public UpdateProductCommand(Guid guid, string name)
    {
        Guid = guid;
        Name = name;
    }
}