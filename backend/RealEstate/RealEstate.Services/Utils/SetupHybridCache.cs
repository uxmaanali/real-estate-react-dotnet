namespace RealEstate.Services.Utils;

using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RealEstate.Shared.Constants;

public static class SetupHybridCache
{
    public static IServiceCollection AddHybridCache(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStrings.RedisConnection)
            ?? throw new InvalidOperationException($"Connection string '{ConnectionStrings.RedisConnection}' not found.");

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionString;
            options.InstanceName = ConnectionStrings.RedisInstanceName;
        });

        services.AddHybridCache(options =>
        {
            options.MaximumPayloadBytes = 1024 * 1024 * 10; // 10MB
            options.MaximumKeyLength = 1024;

            // Default timeouts
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                LocalCacheExpiration = TimeSpan.FromMinutes(30),
                Expiration = TimeSpan.FromHours(1)
            };
        });

        return services;
    }
}
