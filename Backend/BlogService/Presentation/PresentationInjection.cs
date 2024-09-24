using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using SharedResources.Middlewares;

namespace Presentation;

public static class PresentationInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.LoggerConfigure(configuration);
        services.AddScoped<ExceptionHandlingMiddleware>();
        services.AddScoped<LoggingMiddleware>();
        services.AuthorizationConfigure(configuration);
        return services;
    }

    private static IServiceCollection AuthorizationConfigure(this IServiceCollection services,
        IConfiguration configuration)
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

    public static WebApplication StartApplication(this WebApplication webApplication)
    {
        if (webApplication.Environment.IsDevelopment())
        {
            webApplication.UseSwagger();
            webApplication.UseSwaggerUI();
        }

        webApplication.UseMiddleware<ExceptionHandlingMiddleware>();
        webApplication.UseMiddleware<LoggingMiddleware>();

        webApplication.MapControllers();
        webApplication.UseHttpsRedirection();

        webApplication.Run();
        return webApplication;
    }
}