namespace Dotnet.Homeworks.DataAccess.Specs.Infrastructure;

public interface IQueryableFilter<T> where T : class
{
    IQueryable<T> Apply(IQueryable<T> query);
}