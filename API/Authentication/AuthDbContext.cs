using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;

namespace API.Authentication;

public class AuthDbContext : IdentityDbContext<ApplicationUser>
{
    public AuthDbContext()
    {
    }

    public AuthDbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("auth");
        base.OnModelCreating(builder);


        builder.Entity<ApplicationUser>().Ignore(c => c.AccessFailedCount)
            .Ignore(c => c.ConcurrencyStamp)
            .Ignore(c => c.Email)
            .Ignore(c => c.PhoneNumber)
            .Ignore(c => c.LockoutEnabled)
            .Ignore(c => c.PhoneNumberConfirmed)
            .Ignore(c => c.LockoutEnd)
            .Ignore(c => c.NormalizedEmail)
            .Ignore(c => c.PasswordHash)
            .Ignore(c => c.Role)
            .Ignore(c => c.TwoFactorEnabled)
            .Ignore(c => c.SecurityStamp)
            .Ignore(c => c.UserId)
            .Ignore(c => c.UserName)
            .Ignore(c => c.EmailConfirmed);
        builder.Entity<ApplicationUser>()
                .ToTable("Person");

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(@"Data Source=/Users/simohamedkabbou/Studies/S9/Application n-tiers/Projet/JeBalance/DataBase/JeBalance.db");
        }
    }
}