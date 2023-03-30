using LinqKit;
using Microsoft.AspNetCore.Http;
using Movies.Application.Exceptions;
using Movies.Application.Requests.Movies;
using Movies.Application.Responses.Movies;
using Movies.Core.Common;
using Movies.Core.Enums;
using Movies.Core.Models;
using Movies.Core.Repositories;
using System.Net;

namespace Movies.Application.Services.Movies;

public class MoviesServices : IMoviesServices
{
    private readonly IMoviesRepository _repository;
    public MoviesServices(IMoviesRepository repository)
    {
        _repository = repository;
    }

    public Task<PaginationResult<MoviesResponse>> GetMovies(MoviesRequest request)
    {
        var predicate = PredicateBuilder.New<Movie>(true);

        if (!string.IsNullOrEmpty(request.Name))
            predicate.And(x => x.Name.Contains(request.Name));

        if (!string.IsNullOrEmpty(request.Synopsis))
            predicate.And(x => x.Synopsis.Contains(request.Synopsis));

        if (request.CategoryId is not null)
            predicate.And(x => x.CategoryId == request.CategoryId);

        if (request.ReleaseYear is not null)
            predicate.And(x => x.ReleaseYear == request.ReleaseYear);

        if (request.Rating is not null)
            predicate.And(x => x.Rating == request.Rating);

        return _repository.GetAsync<MoviesResponse>(predicate, request);
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

    public async Task UpdateMovie(UpdateMovieRequest request)
    {
        var movie = await _repository.GetByIdAsync(request.Id);

        if (movie is null)
            throw new MoviesException(HttpStatusCode.NotFound, "Movie not found");

        await _repository.Update(movie, request);
    }

    public async Task DeleteMovie(int id)
    {
        var movie = await _repository.GetByIdAsync(id);

        if (movie is null)
            throw new MoviesException(HttpStatusCode.NotFound, "Movie not found");

        await _repository.RemoveAsync(movie);
    }

    public async Task UpdateMovieImage(UpdateMovieImageRequest request)
    {
        var movie = await _repository.GetByIdAsync(request.Id);

        if (movie is null)
            throw new MoviesException(HttpStatusCode.NotFound, "Movie not found");

        var imageData = await GetFilesBytesArray(request.Image);

        movie.Image = imageData;

        await _repository.Update(movie);
    }

    private async Task<byte[]> GetFilesBytesArray(IFormFile file)
    {
        await using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();

        return fileBytes;
    }
}
