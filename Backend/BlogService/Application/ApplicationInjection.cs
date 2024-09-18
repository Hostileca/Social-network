using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.MediatorConfigure();
        services.MapperConfigure();
        services.ValidationConfigure();
        services.MessageBrokerConfigure(configuration);
        
        return services;
    }

    private static IServiceCollection MapperConfigure(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
        
        return services;
    }

    private static IServiceCollection MediatorConfigure(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }

    private static IServiceCollection ValidationConfigure(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();
        
        return services;
    }

    private static IServiceCollection MessageBrokerConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RabbitMqConnection");
        
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) => cfg.Host(connectionString));
        });
        
        return services;
    }
}