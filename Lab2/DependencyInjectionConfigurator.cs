using AutoMapper;
using BusinessLogic.MapperProfile;
using DataAccesLayer.Context;
using DataAccesLayer.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ConsoleApp
{
    public static class DependencyInjectionConfigurator
    {
        public static void Configure(IConfigurationRoot config)
        {
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
        }
    }
}