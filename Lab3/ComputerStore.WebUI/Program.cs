using ComputerStore.WebUI.AppConfiguration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ComputerStore.DataAccessLayer.Models.Identity;

namespace ComputerStore.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<IdentityBuyer>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await IdentityInitializer.InitializeAsync(userManager, roleManager);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                       .ConfigureLogging(logging =>
                                         {
                                             logging.ClearProviders();
                                             logging.AddFile("bin/Logs/StoreApp-{Date}.txt", LogLevel.Error);
                                         })
                       .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}