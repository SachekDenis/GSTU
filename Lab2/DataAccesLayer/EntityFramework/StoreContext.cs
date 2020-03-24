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
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Field> Fields { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryField>()
                .HasKey(t => new { t.CategoryId, t.FieldId });

            modelBuilder.Entity<CategoryField>()
                .HasOne(sc => sc.Category)
                .WithMany(s => s.CategoryFields)
                .HasForeignKey(sc => sc.CategoryId);

            modelBuilder.Entity<CategoryField>()
                .HasOne(sc => sc.Field)
                .WithMany(c => c.CategoryFields)
                .HasForeignKey(sc => sc.FieldId);
        }
    }
}
