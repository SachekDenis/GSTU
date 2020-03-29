using DataAccesLayer.Models;
using Korzh.DbUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

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
                //DbInitializer.Create(dbUtilsOptions =>
                //{
                //    dbUtilsOptions.UseSqlServer(Database.GetDbConnection().ConnectionString);
                //    dbUtilsOptions.UseFileFolderPacker(System.IO.Path.Combine(Environment.CurrentDirectory, "DbSeed"));
                //})
                //.Seed();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(FileLoggerFactory);
        }

        private static readonly ILoggerFactory FileLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder
            .AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information)
            .AddFile("Logs/SQL-{Date}.txt");
        });
    }
}
