namespace Dotnet.Homeworks.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand
{
    public Guid Guid { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public UpdateProductCommand(Guid guid, string name)
    {
        Guid = guid;
        Name = name;
    }
}