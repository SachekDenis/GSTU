using DataAccesLayer.Context;
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
using Microsoft.Extensions.Logging;

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
                .AddTransient(typeof(ManufacturerValidator))
                .AddTransient(typeof(ProductService))
                .AddLogging(config => config.AddFile("Logs/myapp-{Date}.txt"))
                .BuildServiceProvider();

            var manufaturerRepo = services.GetService<IRepository<Manufacturer>>();
            var supplierRepo = services.GetService<IRepository<Supplier>>();
            var supplyRepo = services.GetService<IRepository<Supply>>();

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


            //adminService.DeleteProduct(1);

            Console.WriteLine("Hello World!");
        }
    }
}
