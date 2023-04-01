using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Text.Json;

namespace Movies.Infrastructure.Cache;

public class CacheService : ICacheService
{
    private const int ExpireTimeInSeconds = 60;

    private readonly IDatabase _redisDB;
    public CacheService(IOptions<CacheOptions> options)
    {
        var cache = options.Value;
        var connectionMultiplexer = ConnectionMultiplexer.Connect($"{cache.HostName}:{cache.PortNumber},password={cache.Password}");
        _redisDB = connectionMultiplexer.GetDatabase();
    }

    public async Task<T> GetCacheItemAsync<T>(string key)
    {
        var cacheString = await _redisDB.StringGetAsync(key, CommandFlags.PreferReplica);
        if (string.IsNullOrEmpty(cacheString)) return default(T);
        return JsonSerializer.Deserialize<T>(cacheString);
    }
    public async Task SetCacheItemAsync(string key, object cacheObj)
    {
        var serializedObject = JsonSerializer.Serialize(cacheObj);
        await _redisDB.StringSetAsync(key, serializedObject, TimeSpan.FromSeconds(ExpireTimeInSeconds), When.Always, CommandFlags.PreferMaster);
    }
}
