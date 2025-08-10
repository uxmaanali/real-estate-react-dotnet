using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealEstate.Database.Converters;
using RealEstate.Database.Entities;

namespace RealEstate.Database.EntityConfigs;
public class PropertyConfiguration : BaseEntityConfig<Property>, IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        base.Configuration(builder);

        builder.Property(x => x.Title).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.ListingType).IsRequired();

        builder.Property(c => c.ListingType)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(e => e.Images)
            .HasConversion(new UrlListToStringConverter());
    }
}
