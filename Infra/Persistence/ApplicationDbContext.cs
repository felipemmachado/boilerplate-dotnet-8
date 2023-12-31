﻿using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infra.Persistence;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserService userService) 
    : DbContext(options), IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    private readonly IUserService _userService = userService;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        AddFieldControl();

        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    private void AddFieldControl()
    {
        foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<EntityBase> entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _userService.UserId == Guid.Empty ? null : _userService.UserId;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedBy = _userService.UserId == Guid.Empty ? null : _userService.UserId;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }
    }
}