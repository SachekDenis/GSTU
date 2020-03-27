using DataAccesLayer.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using DataAccesLayer.Repo;
using BusinessLogic.Validation;
using BusinessLogic.Services;
using BusinessLogic.Dto;
using DataAccesLayer.Models;
using AutoMapper;
using BusinessLogic.MapperProfile;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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
