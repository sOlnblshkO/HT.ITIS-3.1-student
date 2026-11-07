using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

namespace Dotnet.Homeworks.Features.Products.Commands.InsertProduct;

public class InsertProductCommand : ICommand<InsertProductDto>
{
    public string Name { get; init; }

    public InsertProductCommand(string name)
    {
        Name = name;
    }
}
