using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

namespace Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;

public class DeleteProductByGuidCommand : ICommand
{
    public Guid Guid { get; init; }

    public DeleteProductByGuidCommand(Guid guid)
    {
        Guid = guid;
    }
}
