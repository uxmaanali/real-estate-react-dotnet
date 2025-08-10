using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealEstate.Database.Abstraction;

namespace RealEstate.Database.EntityConfigs;
public class BaseEntityConfig<T> where T : BaseEntity
{
    public virtual void Configuration(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql("getdate()")
            .IsRequired();

        builder.Property(c => c.ModifiedAt)
            .HasDefaultValueSql("getdate()")
            .IsRequired();

        builder.Property(c => c.UserName)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(x => x.IsActive)
            .IsRequired(true)
            .HasDefaultValue(true);
    }
}
