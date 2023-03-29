using Movies.Application.Commons.Repositories.Common;
using Movies.Core.Common;
using Movies.Core.Models;
using System.Linq.Expressions;

namespace Movies.Core.Repositories;

public interface IMoviesRepository : IRepository<Movie, int>
{
    Task<PaginationResult<TReturn>> GetAsync<TReturn>
        (Expression<Func<Movie, bool>> predicate, PaginationRequest paginationRequest);
}
