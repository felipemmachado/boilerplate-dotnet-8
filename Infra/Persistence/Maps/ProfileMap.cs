using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Persistence.Maps;
public class ProfileMap : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {

        builder.ToTable(nameof(Profile));

        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired().HasColumnType("varchar(100)");
        builder.Property(p => p.Roles).HasColumnType("string[]");

        builder.HasMany(p => p.Users).WithOne(p => p.Profile).HasForeignKey(p => p.ProfileId);
    }
}


