using ComputerStore.DataAccessLayer.Models;
using Korzh.DbUtils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ComputerStore.DataAccessLayer.Context
{
    public class StoreContext : DbContext
    {
        private readonly ILoggerFactory _fileLoggerFactory;

        public StoreContext(DbContextOptions<StoreContext> options, IConfiguration configuration, ILoggerFactory fileLoggerFactory) : base(options)
        {
            _fileLoggerFactory = fileLoggerFactory;
            Database.Migrate();
            DbInitializer.Create(dbUtilsOptions =>
                                 {
                                     dbUtilsOptions.UseSqlServer(Database.GetDbConnection().ConnectionString);
                                     dbUtilsOptions.UseFileFolderPacker(configuration.GetSection("SeedFolder").Value);
                                 })
                         .Seed();
        }

        public DbSet<ProductDto> Products { get; set; }
        public DbSet<OrderDto> Orders { get; set; }
        public DbSet<BuyerDto> Users { get; set; }
        public DbSet<ManufacturerDto> Manufacturers { get; set; }
        public DbSet<CharacteristicDto> Characteristics { get; set; }
        public DbSet<FieldDto> Fields { get; set; }
        public DbSet<CategoryDto> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_fileLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDto>().HasOne<BuyerDto>().WithMany().HasForeignKey(order => order.BuyerId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderDto>().HasOne<ProductDto>().WithMany().HasForeignKey(order => order.ProductId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<CharacteristicDto>()
                        .HasOne<CategoryDto>()
                        .WithMany()
                        .HasForeignKey(characteristic => characteristic.CategoryId)
                        .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FieldDto>().HasOne<CharacteristicDto>().WithMany().HasForeignKey(field => field.CharacteristicId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FieldDto>().HasOne<ProductDto>().WithMany().HasForeignKey(field => field.ProductId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductDto>().HasOne<ManufacturerDto>().WithMany().HasForeignKey(product => product.ManufacturerId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ProductDto>().HasOne<CategoryDto>().WithMany().HasForeignKey(product => product.CategoryId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}