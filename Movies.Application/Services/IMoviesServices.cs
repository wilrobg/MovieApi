using Movies.Application.Requests;
using Movies.Application.Responses;
using Movies.Core.Enums;

namespace Movies.Application.Services;

public interface IMoviesServices
{
    Task AddMovie(AddMovieRequest request);
    IEnumerable<CategoriesResponse> GetMovieCategories();
}
