using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RealEstate.Database.Entities.Context;
using RealEstate.Database.SeedData;
using RealEstate.Shared.Constants;

namespace RealEstate.Database;
public static class DatabaseSetup
{
    public static IServiceCollection RegisterContext(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString(ConnectionStrings.DatabaseConnection)
            ?? throw new InvalidOperationException($"Connection string '{ConnectionStrings.DatabaseConnection}' not found.");

        services.AddDbContext<RealEstateContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));

#if DEBUG
            options.EnableSensitiveDataLogging();
#endif

            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(RealEstateContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null
                );
            });
        });

        return services;
    }

    public static async Task ApplyMigrationsAsync(this IServiceProvider services)
    {
        IEnumerable<string>? pendingMigrations;

        using var scope = services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<RealEstateContext>();

        pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await dbContext.Database.MigrateAsync();
        }
    }

    public static async Task SeedDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        await services.SeedUsers(scope);
        await services.SeedProperies(scope);
    }
}
