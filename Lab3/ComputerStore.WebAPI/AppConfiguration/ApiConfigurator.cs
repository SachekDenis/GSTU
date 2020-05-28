using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ComputerStore.WebAPI.AppConfiguration.SwashbuckleVersioning;
using ComputerStore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ComputerStore.WebAPI.AppConfiguration
{
    public static class ApiConfigurator
    {
        public static void ConfigureApi(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
                                      {
                                          options.ReportApiVersions = true;
                                          options.AssumeDefaultVersionWhenUnspecified = true;
                                          options.DefaultApiVersion = new ApiVersion(1, 0);
                                          options.ApiVersionReader = new UrlSegmentApiVersionReader();
                                          options.UseApiBehavior = true;
                                      });

            services.AddAuthentication()
                    .AddJwtBearer(cfg => cfg.TokenValidationParameters = new TokenValidationParameters
                                                                         {
                                                                             ValidIssuer = JwtInfo.Issuer,
                                                                             ValidAudience = JwtInfo.Audience,
                                                                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfo.Key))
                                                                         });

            services.AddSwaggerGen(swagger =>
                                   {
                                       swagger.SwaggerDoc("v1", new OpenApiInfo {Title = "Store Api", Version = "v1"});

                                       swagger.OperationFilter<VersionOperationFilter>();

                                       swagger.DocumentFilter<VersionDocumentFilter>();

                                       swagger.DocInclusionPredicate((version, desc) =>
                                                                     {
                                                                         if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                                                                         var versions = methodInfo.DeclaringType
                                                                                                  .GetCustomAttributes(true)
                                                                                                  .OfType<ApiVersionAttribute>()
                                                                                                  .SelectMany(attr => attr.Versions);
                                                                         return versions.Any(v => $"v{v.ToString()}" == version);
                                                                     });

                                       swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                                                               {
                                                                                   Description = "JWT Authorization header using the Bearer scheme. " +
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