using Microsoft.Extensions.Caching.Distributed;
using ShoppingProject.Application.Common.Interfaces;

namespace ShoppingProject.Infrastructure.Services;

public class RedisCacheServiceImplementation : IRedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheServiceImplementation(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<string?> GetValueAsync(string key)
    {
        return await _cache.GetStringAsync(key);
    }

    public async Task<bool> SetValueAsync(string key, string value)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
        };

        await _cache.SetStringAsync(key, value, options);
        return true;
    }

    public async Task Clear(string key)
    {
        await _cache.RemoveAsync(key);
    }

    public void ClearAll()
    {
        // Note: IDistributedCache doesn't support clearing all keys
        // This is a limitation when using distributed cache
        // In production, you might need to track keys separately or use Redis directly
    }
}
