using BusinessLogicLayer.Entities;
using BusinessLogicLayer.IdentityServer;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccessLayer;

public static class DataAccessLayerInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.IdentityConfigure();
        services.IdentityServerConfigure(configuration);
        services.DbConfigure(configuration);

        return services;
    }
    
    private static IServiceCollection DbConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlConnectionBuilder = new SqlConnectionStringBuilder
        {
            ConnectionString = configuration.GetConnectionString("SQLDbConnection")
        };
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(sqlConnectionBuilder.ConnectionString));
         
        return services;
    }
    
    private static IServiceCollection IdentityConfigure(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequiredLength = 4;
                config.Password.RequireLowercase = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
         
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
                        sqlOptions => sqlOptions.MigrationsAssembly(typeof(DataAccessLayerInjection).Assembly));
                option.EnableTokenCleanup = true;
                option.TokenCleanupInterval = 3600;
            });

        return services;
    }
}