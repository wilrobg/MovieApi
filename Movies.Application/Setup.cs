using Microsoft.Extensions.DependencyInjection;
using Movies.Application.Services.Movies;
using Movies.Application.Services.Users;

namespace Movies.Application;

public static class Setup
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMoviesServices, MoviesServices>();
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<IJwtProvider, JwtProvider>();
    }
}
