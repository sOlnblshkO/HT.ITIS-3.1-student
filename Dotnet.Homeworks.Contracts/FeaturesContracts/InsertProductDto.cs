namespace Dotnet.Homeworks.Contracts.FeaturesContracts;

public record InsertProductDto
{
    public Guid Id { get; set; }
    public InsertProductDto(Guid id) => Id = id;
}