using Movies.Application.Requests.Movies;
using Movies.Application.Responses.Movies;
using Movies.Core.Common;

namespace Movies.Application.Services.Movies;

public interface IMoviesServices
{
    Task<PaginationResult<MoviesResponse>> GetMovies(MoviesRequest request);
    Task AddMovie(AddMovieRequest request);
    IEnumerable<CategoriesResponse> GetMovieCategories();
    Task UpdateMovie(UpdateMovieRequest request);
    Task DeleteMovie(int id);
    Task UpdateMovieImage(UpdateMovieImageRequest request);
}
