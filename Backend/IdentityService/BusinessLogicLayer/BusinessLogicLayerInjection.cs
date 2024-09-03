using System.Reflection;
using BusinessLogicLayer.IdentityServer;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MapsterMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer;

public static class BusinessLogicLayerInjection
{
     public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, 
         IConfiguration configuration)
     {
         services.IdentityServerConfigure(configuration);
         services.AddServices();
         services.AutoMapperConfigure();
         services.ValidationConfigure();
         services.AddAuthentication()
             .AddCookie();
         services.AddAuthorization();
        
         return services;
     }
     
     private static IServiceCollection ValidationConfigure(this IServiceCollection services)
     {
         services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
         services.AddFluentValidationAutoValidation();
         return services;
     }
     
     private static IServiceCollection AddServices(this IServiceCollection services)
     {
         services.AddScoped<IAccountService, AccountService>();
         
         return services;
     }

     private static IServiceCollection AutoMapperConfigure(this IServiceCollection services)
     {
         var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
         typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
         var mapperConfig = new Mapper(typeAdapterConfig);
         services.AddSingleton<IMapper>(mapperConfig);
         
         return services;
     }
     
     private static IServiceCollection IdentityServerConfigure(this IServiceCollection services, IConfiguration configuration)
     {
         var sqlConnectionBuilder = new SqlConnectionStringBuilder
         {
             ConnectionString = configuration.GetConnectionString("SQLDbConnection")
         };
         services.AddIdentityServer()
             .AddDeveloperSigningCredential()
             .AddAspNetIdentity<User>()
             .AddExtensionGrantValidator<EmailPasswordGrant>()
             .AddInMemoryClients(IdentityConfiguration.Clients)
             .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
             .AddOperationalStore(option =>
             {
                 option.ConfigureDbContext = builder =>
                     builder.UseSqlServer(sqlConnectionBuilder.ConnectionString, 
                         sqlOptions => sqlOptions.MigrationsAssembly(typeof(DataAccessLayerInjection).Assembly.FullName));
                 option.EnableTokenCleanup = true;
                 option.TokenCleanupInterval = 3600;
             });

         return services;
     }
}