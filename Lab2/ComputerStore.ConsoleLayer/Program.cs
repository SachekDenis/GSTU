using System.IO;
using System.Threading.Tasks;
using ComputerStore.ConsoleLayer.Configuration;
using ComputerStore.ConsoleLayer.ConsoleView.BaseConsoleServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerStore.ConsoleLayer
{
    internal class Program
    {
        private const string ConfigFileName = "appsettings.json";

        private static async Task Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile(ConfigFileName);
            var config = builder.Build();

            var services = DependencyInjectionConfigurator.Configure(config);

            var mainMenu = services.GetService<MainMenuBaseConsoleService>();
            await mainMenu.StartConsoleLoop();
        }
    }
}