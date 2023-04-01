using Microsoft.Extensions.DependencyInjection;
using Movies.Infrastructure.Authentication;
using Movies.Infrastructure.Cache;
using StackExchange.Redis;

namespace Movies.Infrastructure;

public static class Setup
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<ICacheService, CacheService>();
    }
}
