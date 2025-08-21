using Microsoft.EntityFrameworkCore;

using RealEstate.Database.Converters;
using RealEstate.Database.Utils;

namespace RealEstate.Database.Entities.Context;
public class RealEstateContext : DbContext
{
    public RealEstateContext(DbContextOptions<RealEstateContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<Favorite> Favorites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseSetup).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTimeOffset>()
            .HaveConversion<DateTimeOffsetConverter>();
    }
}
