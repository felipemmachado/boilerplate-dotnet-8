using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Persistence.Maps;
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable("User");

        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id);

        var fieldConverter = new ValueConverter<IEnumerable<string>, string>(
            v => string.Join("|", v),
            v => v.Split("|", StringSplitOptions.RemoveEmptyEntries));

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired().HasColumnType("varchar(100)");
        builder.Property(p => p.Email).HasMaxLength(100).IsRequired().HasColumnType("varchar(100)");

        builder.Property(p => p.DisabledAt).HasColumnType("timestamptz");
        builder.Property(p => p.FirstAccess).HasColumnType("timestamptz");
        builder.Property(p => p.LastAccess).HasColumnType("timestamptz");

        builder.Property(p => p.CreatedAt).IsRequired().HasColumnType("timestamptz");
        builder.Property(p => p.CreatedBy);
        builder.Property(p => p.UpdatedAt).HasColumnType("timestamptz");
        builder.Property(p => p.UpdatedBy);

        builder.HasIndex(p => p.Email).IsUnique();
    }
}

