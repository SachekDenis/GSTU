using Microsoft.Extensions.Configuration;
using System;
using System.IO;

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

            DependencyInjectionConfigurator.Configure(config);

            Console.WriteLine("Hello World!");
        }
    }
}