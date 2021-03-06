﻿using Domain.Client;
using Domain.House;
using Domain.Network;
using Domain.Payment;
using Domain.Tariff;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Tariff> Tariffs { get; set; }

        public DbSet<Subnet> Subnets { get; set; }
        public DbSet<GreyAddress> GreyAddresses { get; set; }
        public DbSet<WhiteAddress> WhiteAddresses { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<JoinClientsTariffs> JoinClientsTariffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=lightbilling;Username=lightbilling;Password=testPass");
            }

            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GreyAddress>()
                .HasOne(g => g.White)
                .WithOne(w => w.GrayAddress)
                .HasForeignKey<WhiteAddress>(k => k.GrayAddressId);

            modelBuilder.Entity<Client>()
                .HasOne(g => g.GreyAddress)
                .WithOne(w => w.Client)
                .HasForeignKey<GreyAddress>(k => k.ClientId);

            modelBuilder.Entity<Client>()
                .HasOne(a => a.GreyAddress)
                .WithMany()
                .HasForeignKey(k => k.GreyAddressId);

            modelBuilder.Entity<Client>()
                .HasIndex(i => new {i.GreyAddressId})
                .IsUnique();

            modelBuilder.Entity<JoinClientsTariffs>()
                .HasKey(pt => new {pt.ClientId, pt.TariffId});

            modelBuilder.Entity<JoinClientsTariffs>()
                .HasOne(pt => pt.Client)
                .WithMany(p => p.JoinTariffs)
                .HasForeignKey(pt => pt.ClientId);

            modelBuilder.Entity<JoinClientsTariffs>()
                .HasOne(pt => pt.Tariff)
                .WithMany(p => p.JoinClients)
                .HasForeignKey(pt => pt.TariffId);
        }
    }
}