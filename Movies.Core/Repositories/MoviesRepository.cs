using AutoMapper;
using Movies.Core.Contexts;
using Movies.Core.Models;
using Movies.Core.Repositories.Common;

namespace Movies.Core.Repositories;

public class MoviesRepository : Repository<Movie, int>, IMoviesRepository
{
    public MoviesRepository(MoviesContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
