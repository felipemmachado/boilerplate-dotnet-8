using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence;
public static class AppSeedData
{
    public static ModelBuilder AppSeedDataBaseConstructor(this ModelBuilder modelBuilder, IPasswordService _passwordService)
    {
        var createAt = new DateTime(2023, 10, 10, 12, 00, 00, DateTimeKind.Utc);

        var adminProfile = new Profile("Administrador", true, null)
        {
            Id = new Guid("3bec3b12-26c7-4cd9-875e-f60807c0613c"),
            CreatedAt = createAt
        };

        var adminUser = new User(
            adminProfile.Id,
            "Administardor",
            "admin@gmail.com",
            _passwordService.Hash("Admin@123"))
        {
            Id = new Guid("46c807d5-08df-4cd0-a4ae-d51703ce8a4d"),
            CreatedAt = createAt
        };

        modelBuilder.Entity<Profile>().HasData(adminProfile);
        modelBuilder.Entity<User>().HasData(adminUser);


        return modelBuilder;
    }
}
