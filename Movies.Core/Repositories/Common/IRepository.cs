using System.Linq.Expressions;

namespace Movies.Application.Commons.Repositories.Common;

public interface IRepository<TDbSet, TPKey> where TDbSet : class
{
    Task<List<TReturn>> GetAsync<TReturn>(Expression<Func<TDbSet, bool>> predicate);
    Task AddAsync<T>(T requestEntity);
    void Update(TDbSet entity);
    void RemoveAsync(TDbSet entity);
}
