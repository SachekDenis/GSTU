using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using DataAccesLayer.Models;
using Korzh.DbUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Logging;

namespace DataAccesLayer.Context
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Buyer> Users { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Category> Categories { get; set; }

        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                DbInitializer.Create(options =>
                {
                    options.UseSqlServer(Database.GetDbConnection().ConnectionString);
                    options.UseFileFolderPacker(System.IO.Path.Combine(Environment.CurrentDirectory, "DbSeed")); //set the folder where to get the seeding data
                })
                .Seed();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(FileLoggerFactory);
        }

        public static readonly ILoggerFactory FileLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
            .AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information)
            .AddFile("Logs/SQL-{Date}.txt");
        });
    }
}
