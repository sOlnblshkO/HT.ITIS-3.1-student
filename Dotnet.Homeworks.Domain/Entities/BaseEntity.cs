namespace Dotnet.Homeworks.Domain.Entities;

public abstract class BaseEntity
{
    /// <summary>
    /// Id сущности
    /// </summary>
    public Guid Id { get; init; }
    
    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}