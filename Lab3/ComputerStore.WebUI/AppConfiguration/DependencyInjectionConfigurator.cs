using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using ComputerStore.BusinessLogicLayer.MapperProfile;
using ComputerStore.DataAccessLayer.Context;
using ComputerStore.DataAccessLayer.Models.Identity;
using ComputerStore.DataAccessLayer.Repo;
using ComputerStore.WebUI.Models.JwtToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace ComputerStore.WebUI.AppConfiguration
{
    public static class DependencyInjectionConfigurator
    {
        public static void ConfigureAppServices(this IServiceCollection services, IConfiguration config)
        {
            var businessAssembly = Assembly.Load("ComputerStore.BusinessLogicLayer");

            services.AddDbContext<StoreContext>(options => options.UseSqlServer(config.GetConnectionString("StoreConnection")))
                    .Scan(scan => scan.FromAssemblies(businessAssembly).AddClasses(classes => classes.Where(type => type.Name.EndsWith("Manager"))).AsSelf().WithScopedLifetime())
                    .Scan(scan => scan.FromAssemblies(businessAssembly)
                                      .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Validator")))
                                      .AsImplementedInterfaces()
                                      .WithTransientLifetime())
                    .AddAutoMapper(typeof(StoreProfile))
                    .AddScoped(typeof(IRepository<>), typeof(StoreRepository<>));

            services.AddIdentity<IdentityBuyer, IdentityRole>(options => { options.Password.RequireNonAlphanumeric = false; }).AddEntityFrameworkStores<StoreContext>();

            services.AddAuthentication()
                    .AddCookie()
                    .AddJwtBearer(cfg => cfg.TokenValidationParameters = new TokenValidationParameters()
                                                                         {
                                                                             ValidIssuer = JwtInfo.Issuer,
                                                                             ValidAudience = JwtInfo.Audience,
                                                                             IssuerSigningKey =
                                                                                 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.Key))
                                                                         });

            services.AddSwaggerGen(swagger =>
                                   {
                                       swagger.SwaggerDoc("v1", new OpenApiInfo {Title = "Store Api", Version = "v1"});

                                       swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                                                               {
                                                                                   Description =
                                                                                       "JWT Authorization header using the Bearer scheme. " +
                                                                                       "\r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." +
                                                                                       "\r\n\r\nExample: \"Bearer 12345abcdef\"",
                                                                                   Name = "Authorization",
                                                                                   In = ParameterLocation.Header,
                                                                                   Type = SecuritySchemeType.ApiKey,
                                                                                   Scheme = "Bearer"
                                                                               });

                                       swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                                                                      {
                                                                          {
                                                                              new OpenApiSecurityScheme
                                                                              {
                                                                                  Reference = new OpenApiReference
                                                                                              {
                                                                                                  Type = ReferenceType.SecurityScheme,
                                                                                                  Id = "Bearer"
                                                                                              },
                                                                                  Scheme = "oauth2",
                                                                                  Name = "Bearer",
                                                                                  In = ParameterLocation.Header
                                                                              },
                                                                              new List<string>()
                                                                          }
                                                                      });
                                   });
        }
    }
}