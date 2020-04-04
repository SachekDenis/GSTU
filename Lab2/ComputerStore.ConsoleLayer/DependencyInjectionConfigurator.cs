using AutoMapper;
using ComputerStore.BusinessLogicLayer.MapperProfile;
using ComputerStore.DataAccessLayer.Context;
using ComputerStore.DataAccessLayer.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ComputerStore.ConsoleLayer
{
    public static class DependencyInjectionConfigurator
    {
        public static ServiceProvider Configure(IConfigurationRoot config)
        {
            var businessAssembly = Assembly.Load("ComputerStore.BusinessLogicLayer");
            var consoleAssembly = Assembly.Load("ComputerStore.ConsoleLayer");

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
            .AddSingleton(config)
            .AddAutoMapper(typeof(StoreProfile))
            .AddScoped(typeof(IRepository<>), typeof(StoreRepository<>))
            .AddLogging(loggingBuilder => loggingBuilder.AddFile("Logs/StoreApp-{Date}.txt"))
            .BuildServiceProvider();
        }
    }
}