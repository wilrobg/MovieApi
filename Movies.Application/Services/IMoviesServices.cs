using Movies.Application.Requests;
using Movies.Application.Responses;
using Movies.Core.Common;
using Movies.Core.Models;

namespace Movies.Application.Services;

public interface IMoviesServices
{
    Task<PaginationResult<MoviesResponse>> GetMovies(MoviesRequest request);
    Task AddMovie(AddMovieRequest request);
    IEnumerable<CategoriesResponse> GetMovieCategories();
    Task UpdateMovie(UpdateMovieRequest request);
    Task DeleteMovie(int id);
    Task UpdateMovieImage(UpdateMovieImageRequest request);
}
