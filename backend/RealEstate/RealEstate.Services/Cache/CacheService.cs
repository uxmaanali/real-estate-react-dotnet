namespace RealEstate.Services.Cache;

using Microsoft.Extensions.Caching.Hybrid;

using RealEstate.Shared.Abstraction;

public class CacheService : ICacheService, ISingletonDependency, IService
{
    private readonly HybridCache _hybridCache;

    public CacheService(HybridCache hybridCache)
    {
        _hybridCache = hybridCache;
    }


    public async Task<T> GetOrCreateAsync<T>(string key, T data, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Key cannot be null or empty", nameof(key));

        if (data is null)
            throw new ArgumentException("Data cannot be null");

        return await _hybridCache.GetOrCreateAsync(
            key,
            async token => await Task.FromResult(data),
            cancellationToken: cancellationToken
        );
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var options = new HybridCacheEntryOptions
        {
            Expiration = expiration,
        };

        await _hybridCache.SetAsync(key, value, options, cancellationToken: cancellationToken);
    }

    public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        return await _hybridCache.GetOrCreateAsync(
            key,
            async _ => await Task.FromResult<T>(default),
            cancellationToken: cancellationToken
        );
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _hybridCache.RemoveAsync(key, cancellationToken);
    }
}
