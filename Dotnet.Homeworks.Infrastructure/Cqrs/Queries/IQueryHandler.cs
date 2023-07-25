using Dotnet.Homeworks.Infastructure.Utils;
using MediatR;

namespace Dotnet.Homeworks.Infrastructure.Cqrs.Queries;


public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}