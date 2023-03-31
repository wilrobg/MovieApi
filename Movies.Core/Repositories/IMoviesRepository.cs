using Movies.Application.Commons.Repositories.Common;
using Movies.Core.Common;
using Movies.Core.Enums;
using Movies.Core.Models;

namespace Movies.Core.Repositories;

public interface IMoviesRepository : IRepository<Movie, int>
{
    Task<PaginationResult<TReturn>> GetAsync<TReturn>(
        PaginationRequest paginationRequest,
        string name,
        string synopsis,
        MovieCategory? categoryId,
        int? releaseYear,
        short? rating,
        string createdBy,
        string orderBy,
        string orderByDesc);
}
