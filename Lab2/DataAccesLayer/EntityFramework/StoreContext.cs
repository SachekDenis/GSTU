using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using DataAccesLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace DataAccesLayer.EntityFramework
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Buyer> Users { get; set; }
        public DbSet<GraphicalCard> GraphicalCards { get; set; }
        public DbSet<Cpu> Cpus { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<Ram> Rams { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Characteristics> Characteristics { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
