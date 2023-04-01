namespace Movies.Infrastructure.Cache;

public interface ICacheService
{
    Task<T> GetCacheItemAsync<T>(string key);
    Task SetCacheItemAsync(string key, object cacheObj);
}
