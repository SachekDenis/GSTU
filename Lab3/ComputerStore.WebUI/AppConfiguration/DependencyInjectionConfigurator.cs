using System.Reflection;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.MapperProfile;
using ComputerStore.DataAccessLayer.Context;
using ComputerStore.DataAccessLayer.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ComputerStore.WebUI.AppConfiguration
{
    public static class DependencyInjectionConfigurator
    {
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration config)
        {
            var businessAssembly = Assembly.Load("ComputerStore.BusinessLogicLayer");

            services
                .AddDbContext<StoreContext>(options =>
                    options.UseSqlServer(config.GetConnectionString("StoreConnection")))
                .Scan(scan => scan
                    .FromAssemblies(businessAssembly)
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Manager")))
                    .AsSelf()
                    .WithTransientLifetime())
                .Scan(scan => scan
                    .FromAssemblies(businessAssembly)
                    .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Validator")))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime())
                .AddAutoMapper(typeof(StoreProfile))
                .AddScoped(typeof(IRepository<>), typeof(StoreRepository<>))
                .AddLogging(loggingBuilder => loggingBuilder.AddFile("Logs/StoreApp-{Date}.txt"));
        }
    }
}