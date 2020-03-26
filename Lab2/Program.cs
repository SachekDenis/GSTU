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
using System.Reflection;

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

            var assemblyToScan = Assembly.Load("BusinessLogic");

            var services = new ServiceCollection()
                .AddDbContext<StoreContext>(options =>
                    {
                        options.UseSqlServer(config.GetConnectionString("StoreConnection"));
                    }, ServiceLifetime.Transient)
                .Scan(scan => scan
                    .FromAssemblies(assemblyToScan)
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                    .AsSelf()
                    .WithTransientLifetime())
                .AddAutoMapper(typeof(StoreProfile))
                .AddSingleton(typeof(IRepository<>), typeof(StoreRepository<>))
                .AddLogging(config => config.AddFile("Logs/myapp-{Date}.txt"))
                .BuildServiceProvider();

            var manufaturerRepo = services.GetService<IRepository<Manufacturer>>();
            var supplierRepo = services.GetService<IRepository<Supplier>>();
            var supplyRepo = services.GetService<IRepository<Supply>>();
            var productService = services.GetService<ProductService>();

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
