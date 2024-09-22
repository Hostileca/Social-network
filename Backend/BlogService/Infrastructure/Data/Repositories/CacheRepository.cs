using Domain.Repositories;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Data.Repositories;

public class CacheRepository(
    IConnectionMultiplexer redis) : ICacheRepository
{
    private readonly IDatabase redisDatabase = redis.GetDatabase();
    
    public async Task SetAsync<TObject>(string key, TObject value, TimeSpan expiresIn)
    {
        string serializedData = JsonSerializer.Serialize(value);
        await redisDatabase.StringSetAsync(Key<TObject>(key), serializedData, expiresIn);
    }

    public async Task<TObject> GetAsync<TObject>(string key)
    {
        var redisValue = await redisDatabase.StringGetAsync(Key<TObject>(key));
        var serializedData = redisValue.ToString();
        return serializedData == null ? default : JsonSerializer.Deserialize<TObject>(serializedData);
    }

    private string Key<TObject>(string key) => $"{typeof(TObject)}_{key}";
}