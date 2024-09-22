using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
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
        services.AddScoped<ExceptionHandlingMiddleware>();
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
}