using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

namespace Dotnet.Homeworks.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand : ICommand
{
    public Guid Guid { get; init; }
    public string Name { get; init; }

    public UpdateProductCommand(Guid guid, string name)
    {
        Guid = guid;
        Name = name;
    }
}
