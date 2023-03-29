using Microsoft.Extensions.DependencyInjection;
using Movies.Core.Contexts;

namespace Movies.Core;

public static class Setup
{
    public static void AddDbContextSetup(this IServiceCollection services) =>
        services.AddDbContext<MoviesContext>();

    public static async Task DatabaseSeeder(this IServiceCollection services)
    {
        using var serviceScope = services.BuildServiceProvider().CreateScope();
        using var context = serviceScope.ServiceProvider.GetRequiredService<MoviesContext>();
        await context.AddSeeder();
    }
}
