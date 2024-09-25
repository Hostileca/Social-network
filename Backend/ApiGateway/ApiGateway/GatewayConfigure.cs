using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway;

public static class GatewayConfigure
{
    public static IServiceCollection ConfigureGateway(this IServiceCollection services, ConfigurationManager configuration)
    {
        configuration.AddJsonConfigs();
        services.OcelotConfigure(configuration);
        services.AuthenticationConfigure(configuration);
        
        return services;
    }

    private static ConfigurationManager AddJsonConfigs(this ConfigurationManager configuration)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        configuration
            .AddJsonFile($"ocelot.{environment}.json", optional: false, reloadOnChange: true);
        
        return configuration;
    }
    private static IServiceCollection OcelotConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOcelot();

        return services;
    }
    
    private static IServiceCollection AuthenticationConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = configuration["JwtSettings:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)),
                ValidateActor = true,
                ValidateIssuer = true,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateIssuerSigningKey = true
            };
        });
        
        return services;
    }

    public static async Task<WebApplication> LaunchGateway(this WebApplication webApplication)
    {
        await webApplication.UseOcelot();
        webApplication.UseHttpsRedirection(); 
        await webApplication.RunAsync();
        
        return webApplication;
    }
}