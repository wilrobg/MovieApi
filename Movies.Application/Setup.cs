﻿using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Services.MovieRates;
using Movies.Application.Services.Movies;
using Movies.Application.Services.Users;

namespace Movies.Application;

public static class Setup
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMoviesServices, MoviesServices>();
        services.AddScoped<IMovieRateServices, MovieRateServices>();
        services.AddScoped<IUserServices, UserServices>();
    }
}
