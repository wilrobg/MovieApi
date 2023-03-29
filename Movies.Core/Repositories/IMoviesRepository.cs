using Movies.Application.Commons.Repositories.Common;
using Movies.Core.Models;

namespace Movies.Core.Repositories;

public interface IMoviesRepository : IRepository<Movie, int>
{
}
