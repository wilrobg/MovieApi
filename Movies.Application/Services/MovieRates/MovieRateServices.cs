using Movies.Infrastructure.HttpContextAccessor;
using Movies.Application.Exceptions;
using Movies.Application.Requests.Rates;
using Movies.Application.Responses.MovieRate;
using Movies.Core.Entities;
using Movies.Core.Repositories;
using System.Net;

namespace Movies.Application.Services.MovieRates;

public class MovieRateServices : IMovieRateServices
{
    private readonly IUserHttpContextAccesor _userHttpContextAccesor;
    private readonly IMoviesRepository _moviesRepository;
    private readonly IMovieRateRepository _movieRateRepository;
    public MovieRateServices(
        IUserHttpContextAccesor userHttpContextAccesor, 
        IMoviesRepository moviesRepository, 
        IMovieRateRepository movieRateRepository)
    {
        _userHttpContextAccesor = userHttpContextAccesor;
        _moviesRepository = moviesRepository;
        _movieRateRepository = movieRateRepository;
    }

    public Task<List<MovieRateResponse>> GetMoviesRate()
    {
        var userId = _userHttpContextAccesor.Id;

        return _movieRateRepository.GetAsync<MovieRateResponse>(x => x.UserId == userId);
    }

    public async Task RateMovie(RateMovieRequest request)
    {
        var movie = await _moviesRepository.GetByIdAsync(request.MovieId);

        if (movie is null)
            throw new MoviesException(HttpStatusCode.NotFound, "Movie not found");

        var userId = _userHttpContextAccesor.Id;

        var movieRate = await _movieRateRepository.FirstOrDefault(x => x.MovieId == movie.Id && x.UserId == userId);

        if (movieRate is null)
        {
            movieRate = MovieRate.Create(movie, userId, request.Rate.Value);
            await _movieRateRepository.AddAsync(movieRate);
        }
        else
        {
            movieRate.Rate = request.Rate.Value;
            movieRate.UserId = _userHttpContextAccesor.Id;
            await _movieRateRepository.Update(movieRate);
        }
    }

    public async Task RemoveRateMovie(int movieId)
    {
        var movie = await _moviesRepository.GetByIdAsync(movieId);

        if (movie is null)
            throw new MoviesException(HttpStatusCode.NotFound, "Movie not found");


        var userId = _userHttpContextAccesor.Id;

        var movieRate = await _movieRateRepository.FirstOrDefault(x => x.MovieId == movie.Id && x.UserId == userId);

        if(movieRate is null) return;

        await _movieRateRepository.RemoveAsync(movieRate);
    }
}
