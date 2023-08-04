namespace Dotnet.Homeworks.Features.Products.Commands.InsertProduct;

public class InsertProductCommand
{
    public string Name { get; init; }

    public InsertProductCommand(string name)
    {
        Name = name;
    }
}
