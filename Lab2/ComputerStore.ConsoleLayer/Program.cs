using System.IO;
using ComputerStore.BusinessLogicLayer.DependencyInjection;
using ComputerStore.ConsoleLayer.ConsoleView;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerStore.ConsoleLayer
{
    internal class Program
    {
        private const string ConfigFileName = "appsettings.json";
        private static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(ConfigFileName);
            var config = builder.Build();

            var services = DependencyInjectionConfigurator.Configure(config);

            var mainMenu = services.GetService<MainMenuConsoleService>();
            mainMenu.StartConsoleLoop();
        }
    }
}