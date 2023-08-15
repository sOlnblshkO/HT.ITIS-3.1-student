namespace Dotnet.Homeworks.Domain.Entities;

public class Order : BaseEntity
{
    /// <summary>
    /// Id пользователя, сделавшего заказ
    /// </summary>
    public Guid OrdererId { get; init; }
    
    /// <summary>
    /// Список Id продуктов, входящих в заказ
    /// </summary>
    public IEnumerable<Guid> ProductsIds { get; init; }
}