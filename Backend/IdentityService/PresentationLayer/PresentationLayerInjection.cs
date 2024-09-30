using System.Text;
using DataAccessLayer;
using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
using DataAccessLayer.gRPC.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using SharedResources.Middlewares;
using SharedResources.Policies;

namespace PresentationLayer;

public static class PresentationLayerInjection
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddControllers();
        services.AuthConfigure(configuration);
        services.AddSwaggerGen();
        services.LoggerConfigure(configuration);
        services.AddScoped<ExceptionHandlingMiddleware>();
        services.AddScoped<LoggingMiddleware>();
        
        return services;
    }
    
    public static WebApplication StartApplication(this WebApplication webApplication)
    {
        webApplication.DbInitialize();
        webApplication.UseAuthentication();
        webApplication.UseAuthorization();
        webApplication.MapControllers();
        webApplication.MapGrpc();
        webApplication.SwaggerStart();
        webApplication.SeedData();
        webApplication.UseMiddleware<ExceptionHandlingMiddleware>();
        webApplication.UseMiddleware<LoggingMiddleware>();
        webApplication.Run();
         
        return webApplication;
    }


    private static IServiceCollection AuthConfigure(this IServiceCollection services, IConfiguration configuration)
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
        services.AddAuthorization(option =>
        {
            option.AddPolicy(Policies.RequireStaff, 
                policy => policy.RequireRole(Roles.Admin));
        });
        
        return services;
    }

    private static IServiceCollection LoggerConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("LogstashConnection");
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.Http(connection, 
                queueLimitBytes: null, 
                textFormatter: new CompactJsonFormatter())
            .CreateLogger();
        
        Log.Logger.Write(LogEventLevel.Information, "Service started");
        return services;
    }
    
    private static WebApplication DbInitialize(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        DbInitializer.Initialize(appDbContext);

        return webApplication;
    }
    
    private static WebApplication SwaggerStart(this WebApplication webApplication)
    {
        if (webApplication.Environment.IsDevelopment())
        {
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();
        }

        return webApplication;
    }
    
    private static WebApplication SeedData(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        DbInitializer.Seed(userManager, roleManager);
        
        return webApplication;
    }
    
    private static WebApplication MapGrpc(this WebApplication webApplication)
    {
        webApplication.MapGrpcService<UserGrpcService>();
        
        return webApplication;
    }
}