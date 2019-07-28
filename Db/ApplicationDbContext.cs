using System;
using Domain;
using Domain.Client;
using Domain.House;
using Domain.Network;
using Domain.Tariff;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Db
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Subnet> Subnets { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=lightbilling;Username=lightbilling;Password=testPass");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
//            modelBuilder.Entity<Subnet>()
//                .HasMany(x => x.Houses)
//                .WithOne();
//            modelBuilder.Entity<Subnet>()
//                .HasOne(x => x.House)
//                .WithOne(y => y.Subnet)
//                .HasForeignKey<House>(x => x.SubnetId);
        }
    }
}