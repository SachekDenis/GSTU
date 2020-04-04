using ComputerStore.DataAccessLayer.Models;
using Korzh.DbUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ComputerStore.DataAccessLayer.Context
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

        public StoreContext(DbContextOptions<StoreContext> options, IConfigurationRoot configuration) : base(options)
        {
            if (Database.EnsureCreated())
            {
                DbInitializer.Create(dbUtilsOptions =>
                    {
                        dbUtilsOptions.UseSqlServer(Database.GetDbConnection().ConnectionString);
                        dbUtilsOptions.UseFileFolderPacker(configuration.GetSection("SeedFolder").Value);
                    })
                .Seed();
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(FileLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne<Buyer>()
                .WithMany()
                .HasForeignKey(order => order.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(order => order.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Characteristic>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(characteristic => characteristic.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Field>()
                .HasOne<Characteristic>()
                .WithMany()
                .HasForeignKey(field => field.CharacteristicId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Field>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(field => field.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne<Manufacturer>()
                .WithMany()
                .HasForeignKey(product => product.ManufacturerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany()
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Supply>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(supply => supply.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Supply>()
                .HasOne<Supplier>()
                .WithMany()
                .HasForeignKey(supplier => supplier.SupplierId)
                .OnDelete(DeleteBehavior.NoAction);
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
