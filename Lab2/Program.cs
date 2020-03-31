﻿using System.IO;
using ComputerStore.ConsoleLayer.ConsoleView;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ComputerStore.ConsoleLayer
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