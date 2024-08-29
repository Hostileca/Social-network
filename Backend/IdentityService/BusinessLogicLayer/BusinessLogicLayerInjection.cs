using System.Reflection;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLogicLayer;

public static class BusinessLogicLayerInjection
{
     public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
     {
         services.AddServices();
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
         services.AddScoped<IAuthService, AuthService>();
         
         return services;
     }
 }