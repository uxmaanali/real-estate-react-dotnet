using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealEstate.Database.Entities;

namespace RealEstate.Database.EntityConfigs;
public class UserConfiguration : BaseEntityConfig<User>, IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configuration(builder);

        builder.Property(x => x.Email).IsRequired();

        builder.Property(x => x.PasswordHash).IsRequired();

        builder.Property(x => x.Role).IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(c => c.Role)
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}
