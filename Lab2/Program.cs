﻿using DataAccesLayer.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using DataAccesLayer.Repo;

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
                })
                .AddSingleton(typeof(IRepository<>),typeof(StoreRepository<>))
                .BuildServiceProvider();

            var context = services.GetService<StoreContext>();


            Console.WriteLine("Hello World!");
        }
    }
}