using System;
using Domain;
using Domain.Client;
using Domain.House;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Db
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<House> Houses { get; set; }
        
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=lightbilling;Username=lightbilling;Password=testPass");
        }
    }
}