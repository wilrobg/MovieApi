using Movies.Application.Requests.Rates;

namespace Movies.Application.Services.MovieRates;

public interface IMovieRateServices
{
    Task RateMovie(RateMovieRequest request);
}
