using ConsoleApp.ConsoleView;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();

            var services = DependencyInjectionConfigurator.Configure(config);

            var mainMenu = services.GetService<MainMenuService>();
             mainMenu.StartMainLoop();
        }
    }
}