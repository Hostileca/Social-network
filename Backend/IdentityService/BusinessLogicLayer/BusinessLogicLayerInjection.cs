using System.Reflection;
using BusinessLogicLayer.Services.Algorithms;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MapsterMapper;

namespace BusinessLogicLayer;

public static class BusinessLogicLayerInjection
{
     public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, 
         IConfiguration configuration)
     {
         services.AddServices();
         services.AutoMapperConfigure();
         services.ValidationConfigure();
        
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
         services.AddScoped<TokensGenerator>();
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
}