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
        services.CorsConfigure();
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
            
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chats/hub"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });
        
        return services;
    }

    private static IServiceCollection CorsConfigure(this IServiceCollection services)
    {
        services
            .AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        
        return services;
    }

    public static async Task<WebApplication> LaunchGateway(this WebApplication webApplication)
    {
        webApplication.UseCors("AllowSpecificOrigin");
        webApplication.UseWebSockets();
        await webApplication.UseOcelot();
        webApplication.UseHttpsRedirection(); 
        await webApplication.RunAsync();
        
        return webApplication;
    }
}