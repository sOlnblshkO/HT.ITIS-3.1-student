namespace Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

public interface ICommandHandler<in TCommand> //TODO: Inherit certain interface 
{
}

public interface ICommandHandler<in TCommand, TResponse> //TODO: Inherit certain interface 
{
}