using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Entities;
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
}