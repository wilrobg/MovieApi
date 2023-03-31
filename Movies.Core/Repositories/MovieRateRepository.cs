using AutoMapper;
using Movies.Core.Contexts;
using Movies.Core.Entities;
using Movies.Core.Repositories.Common;

namespace Movies.Core.Repositories;

public class MovieRateRepository : Repository<MovieRate, int>, IMovieRateRepository
{
    public MovieRateRepository(MoviesContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
