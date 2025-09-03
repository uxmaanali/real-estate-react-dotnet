namespace RealEstate.Database.EntityConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealEstate.Database.Abstraction;

public class BaseEntityConfig<T> where T : AuditableEntity
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

        builder.Property(c => c.CreatedBy)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(c => c.ModifiedBy)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.Property(x => x.IsActive)
            .IsRequired(true)
            .HasDefaultValue(true);
    }
}
