namespace Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

public interface ICommandHandler<in TCommand> // : ???
{
}

public interface ICommandHandler<in TCommand, TResponse> // : ???
{
}