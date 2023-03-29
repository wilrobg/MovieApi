using Movies.Application.Requests;
using Movies.Application.Responses;
using Movies.Core.Enums;
using Movies.Core.Repositories;

namespace Movies.Application.Services;

public class MoviesServices : IMoviesServices
{
    private readonly IMoviesRepository _repository;
    public MoviesServices(IMoviesRepository repository)
    {
        _repository = repository;
    }

    public Task AddMovie(AddMovieRequest request)
    {
        return _repository.AddAsync(request);
    }

    public IEnumerable<CategoriesResponse> GetMovieCategories()
    {
        var movieCategories = Enum.GetValues<MovieCategory>()
            .Select(m => new CategoriesResponse(m, m.GetEnumDescription()));

        return movieCategories;
    }
}
