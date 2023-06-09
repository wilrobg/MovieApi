﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Movies.Core.Contexts;
using Movies.Core.Entities;
using Movies.Core.Repositories;
using Movies.Core.Seeders;

namespace Movies.Core;

public static class Setup
{
    public static void AddDbContextSetup(this IServiceCollection services)
    {
        services.AddDbContext<MoviesContext>();

        services.AddScoped<IMoviesRepository, MoviesRepository>();
        services.AddScoped<IMovieRateRepository, MovieRateRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddIdentity<User, Role>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<MoviesContext>();
    }

    public static async Task DatabaseSeeder(this IServiceCollection services)
    {
        using var serviceScope = services.BuildServiceProvider().CreateScope();


        using var context = serviceScope.ServiceProvider.GetRequiredService<MoviesContext>();
        context.Database.EnsureCreated();

        var userRepository = (UserRepository)serviceScope.ServiceProvider.GetRequiredService<IUserRepository>();
        await userRepository.Seed();

        await context.Seed();
    }
}
