using Microsoft.Extensions.DependencyInjection;
using Movies.Core.Contexts;
using Movies.Core.Repositories;

namespace Movies.Core;

public static class Setup
{
    public static void AddDbContextSetup(this IServiceCollection services)
    {
        services.AddDbContext<MoviesContext>();

        services.AddScoped<IMoviesRepository, MoviesRepository>();
    }

    public static async Task DatabaseSeeder(this IServiceCollection services)
    {
        using var serviceScope = services.BuildServiceProvider().CreateScope();
        using var context = serviceScope.ServiceProvider.GetRequiredService<MoviesContext>();
        await context.AddSeeder();
    }
}
