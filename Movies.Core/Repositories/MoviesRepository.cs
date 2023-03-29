using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Movies.Core.Common;
using Movies.Core.Contexts;
using Movies.Core.Models;
using Movies.Core.Repositories.Common;
using System.Linq.Expressions;

namespace Movies.Core.Repositories;

public class MoviesRepository : Repository<Movie, int>, IMoviesRepository
{
    public MoviesRepository(MoviesContext context, IMapper mapper) : base(context, mapper)
    {
    }

    async Task<PaginationResult<TReturn>> IMoviesRepository.GetAsync<TReturn>(Expression<Func<Movie, bool>> predicate, PaginationRequest paginationRequest)
    {
        var query = GetAsync<TReturn>(predicate);

        var data = await query
        .Skip((paginationRequest.PageNumber - 1) * paginationRequest.PageSize)
        .Take(paginationRequest.PageSize)
        .ToListAsync();

        var totalRecords = await query.CountAsync();

        return PaginationResult<TReturn>.GetPagination(data, paginationRequest.PageNumber, paginationRequest.PageSize, totalRecords);
    }
}
