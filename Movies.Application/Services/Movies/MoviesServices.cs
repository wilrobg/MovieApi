﻿using Microsoft.AspNetCore.Http;
using Movies.Application.Exceptions;
using Movies.Application.Requests.Movies;
using Movies.Application.Responses.Movies;
using Movies.Core.Common;
using Movies.Core.Enums;
using Movies.Core.Models;
using Movies.Core.Repositories;
using Movies.Core.Abstractions;
using System.Net;
using Movies.Infrastructure.HttpContextAccessor;
using Movies.Infrastructure.Cache;

namespace Movies.Application.Services.Movies;

public class MoviesServices : IMoviesServices
{
    private readonly IMoviesRepository _repository;
    private readonly IUserHttpContextAccesor _userHttpContext;
    private readonly ICacheService _cacheService;

    public MoviesServices(
        IMoviesRepository repository,
        IUserHttpContextAccesor userHttpContext,
        ICacheService cacheService)
    {
        _repository = repository;
        _userHttpContext = userHttpContext;
        _cacheService = cacheService;
    }

    public async Task<PaginationResult<MoviesResponse>> GetMovies(MoviesRequest request)
    {
        var key = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
            request.Name,
            request.Synopsis,
            request.CategoryId,
            request.ReleaseYear,
            request.Rating,
            request.CreatedBy,
            request.OrderBy,
            request.OrderByDesc);

        var results = await _cacheService.GetCacheItemAsync<PaginationResult<MoviesResponse>>(key);
        
        if(results is null)
        {
            results = await _repository.
                GetAsync<MoviesResponse>(
                request,
                request.Name,
                request.Synopsis,
                request.CategoryId,
                request.ReleaseYear,
                request.Rating,
                request.CreatedBy,
                request.OrderBy,
                request.OrderByDesc);

            await _cacheService.SetCacheItemAsync(key, results);
        }

        return results;
    }

    public Task AddMovie(AddMovieRequest request)
    {
        var movie = Movie.Create(
            request.Name, 
            request.ReleaseYear.Value, 
            request.Synopsis, 
            request.CategoryId.Value, 
            _userHttpContext.Email);

        return _repository.AddAsync(movie);
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
