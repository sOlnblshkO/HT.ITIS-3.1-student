using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}