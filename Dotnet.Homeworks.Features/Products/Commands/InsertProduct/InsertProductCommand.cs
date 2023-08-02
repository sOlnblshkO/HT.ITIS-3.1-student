namespace Dotnet.Homeworks.Features.Products.Commands.InsertProduct;

public class InsertProductCommand
{
    public string Name { get; set; } = string.Empty;

    public InsertProductCommand(string name)
    {
        Name = name;
    }
}
