﻿using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Services.Movies;

namespace Movies.Application;

public static class Setup
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMoviesServices, MoviesServices>();
    }
}
