namespace Dotnet.Homeworks.MessagingContracts.FeaturesContracts;

public record GetProductDto 
{
    public GetProductDto(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    
}