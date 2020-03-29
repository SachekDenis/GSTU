using AutoMapper;
using BusinessLogic.MapperProfile;
using ConsoleApp.ConsoleView;
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
        public static ServiceProvider Configure(IConfigurationRoot config)
        {
            var businessAssembly = Assembly.Load("BusinessLogic");
            var consoleAssembly = Assembly.Load("ConsoleApp");

            return new ServiceCollection()
            .AddDbContext<StoreContext>(options =>
                options.UseSqlServer(config.GetConnectionString("StoreConnection")))
            .Scan(scan => scan
                .FromAssemblies(businessAssembly, consoleAssembly)
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Manager")))
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Validator")))
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("ConsoleService")))
                .AsSelf()
                .WithTransientLifetime())
            .AddAutoMapper(typeof(StoreProfile))
            .AddScoped(typeof(IRepository<>), typeof(StoreRepository<>))
            .AddScoped(typeof(MainMenuService))
            .AddLogging(config => config.AddFile("Logs/myapp-{Date}.txt"))
            .BuildServiceProvider();
        }
    }
}