using Movies.Application.Requests.Rates;
using Movies.Application.Responses.MovieRate;

namespace Movies.Application.Services.MovieRates;

public interface IMovieRateServices
{
    Task<List<MovieRateResponse>> GetMoviesRate();
    Task RateMovie(RateMovieRequest request);
    Task RemoveRateMovie(int movieId);
}
