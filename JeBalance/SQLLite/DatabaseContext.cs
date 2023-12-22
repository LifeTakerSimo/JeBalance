using System;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using JeBalance.SQLLite.Configuration;
using JeBalance.SQLLite.Model;

namespace JeBalance.SQLLite
{
	public class DatabaseContext : DbContext
    {
        public const string DEFAULT_SCHEMA = "app";

        public DbSet<PersonSQLS> Personnes { get; set; }
        public DbSet<DenonciationSQLS> Denonciations { get; set; }
        public DbSet<ResponseSQLS> Responses { get; set; }
        public DbSet<CalomniateurSQLS> Calomniateurs { get; set; }
        public DbSet<AdminSQLS> Admins { get; set; }

        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.ApplyConfiguration(new PersonConfiguration());
           modelBuilder.ApplyConfiguration(new DenonciationConfiguration());
           modelBuilder.ApplyConfiguration(new ResponseConfiguration());
           modelBuilder.ApplyConfiguration(new CalomniateurConfiguration());
           modelBuilder.ApplyConfiguration(new AdminConfiguration());


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=/Users/simohamedkabbou/Studies/S9/Application n-tiers/Projet/JeBalance/DataBase/JeBalance.db");
            }
        }
    } 
}

