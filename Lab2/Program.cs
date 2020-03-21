using DataAccesLayer.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using DataAccesLayer.Repo;
using BusinessLogic.Validation;
using BusinessLogic.Services;
using BusinessLogic.Dto;
using DataAccesLayer.Models;
using AutoMapper;
using BusinessLogic.MapperProfile;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var services = new ServiceCollection()
                .AddDbContext<StoreContext>(options =>
                {
                    options.UseSqlServer(config.GetConnectionString("StoreConnection"));
                }, ServiceLifetime.Transient)
                .AddSingleton(typeof(IRepository<>), typeof(StoreRepository<>))
                .AddTransient(typeof(SupplyValidator))
                .AddAutoMapper(typeof(StoreProfile))
                .AddTransient(typeof(SupplierValidator))
                .AddTransient(typeof(ProductValidator))
                .AddTransient(typeof(RamValidator))
                .AddTransient(typeof(AdminService))
                .BuildServiceProvider();

            var manufaturerRepo = services.GetService<IRepository<Manufacturer>>();
            var supplierRepo = services.GetService<IRepository<Supplier>>();
            var supplyRepo = services.GetService<IRepository<Supply>>();
            var adminService = services.GetService<AdminService>();

            var manufacturer = new Manufacturer()
            {
                Country = "USA",
                Name = "AMD"
            };

            manufaturerRepo.Add(manufacturer);

            var supplier = new Supplier()
            {
                Adress = "Artty",
                Name = "HYT",
                Phone = "+375449233"
            };

            supplierRepo.Add(supplier);

            var ramDto = new RamDto()
            {
                Count = 2,
                Date = DateTime.Now,
                Frequency = 900,
                ManufacturerId = manufacturer.Id,
                Name = "grx",
                Price = 100,
                Timings = "11-11-11",
                Type = "DDR-4",
                Volume = 16,
                SupplierId = supplier.Id
            };

            adminService.AddRam(ramDto);

            Console.WriteLine("Hello World!");
        }
    }
}
