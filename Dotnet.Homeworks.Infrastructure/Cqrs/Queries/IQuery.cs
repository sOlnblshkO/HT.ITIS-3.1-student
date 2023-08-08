using Dotnet.Homeworks.Infrastructure.Utils;
using Dotnet.Homeworks.Mediator;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
    public Result<TResponse> Result { get; init; }
}