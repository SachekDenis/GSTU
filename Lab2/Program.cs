using DataAccesLayer.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using Korzh.DbUtils.Export;
using Korzh.DbUtils.SqlServer;
using ConsoleApp.ConsoleView;
using BusinessLogic.Services;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var services = DependencyInjectionConfigurator.Configure(config);

            var mainMenu = services.GetService<MainMenuService>();
            await mainMenu.StartMainLoop();
            
            Console.WriteLine("Hello World!");
        }
    }
}