﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Persistence.Maps;
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {

        builder.ToTable(nameof(User));

        builder.HasKey(k => k.Id);
        builder.Property(p => p.Id);

        builder.Property(p => p.Name).HasMaxLength(100).IsRequired().HasColumnType("varchar(100)");
        builder.Property(p => p.Email).HasMaxLength(100).IsRequired().HasColumnType("varchar(100)");

        builder.Property(p => p.DisabledAt).HasColumnType("timestamptz");
        builder.Property(p => p.FirstAccess).HasColumnType("timestamptz");
        builder.Property(p => p.LastAccess).HasColumnType("timestamptz");

        builder.HasIndex(p => p.Email).IsUnique();
    }
}

