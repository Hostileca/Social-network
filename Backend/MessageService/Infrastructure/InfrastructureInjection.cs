using Application.SignalR.Services;
using Domain.Repositories;
using Hangfire;
using Hangfire.SqlServer;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.MesssageBroker.Consumers;
using Infrastructure.SignalR.Services;
using MassTransit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.DbConfigure(configuration);
        services.RepositoriesConfigure();
        services.SignalRConfigure();
        services.MessageBrokerConfigure(configuration);
        services.HangfireConfigure(configuration);
        
        return services;
    }

    private static IServiceCollection DbConfigure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var sqlConnectionBuilder = new SqlConnectionStringBuilder
        {
            ConnectionString = configuration.GetConnectionString("SQLDbConnection")
        };
        services.AddDbContext<AppDbContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(sqlConnectionBuilder.ConnectionString)
        );
        
        return services;
    }
    
    private static IServiceCollection RepositoriesConfigure(this IServiceCollection services)
    {
        services.AddScoped<IAttachmentRepository, AttachmentRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<IChatMemberRepository, ChatMemberRepository>();
        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IReactionRepository, ReactionRepository>();
        services.AddScoped<IBlogConnectionRepository, BlogConnectionRepository>();
        
        return services;
    }

    private static IServiceCollection SignalRConfigure(this IServiceCollection services)
    {
        services.AddSignalR();
        
        services.AddScoped<IChatNotificationService, ChatNotificationService>();
        services.AddScoped<IChatMemberNotificationService, ChatMemberNotificationService>();
        services.AddScoped<IMessageNotificationService, MessageNotificationService>();
        services.AddScoped<IReactionNotificationService, ReactionNotificationService>();
        
        return services;
    }

    private static IServiceCollection MessageBrokerConfigure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection("RabbitMqSettings");
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<BlogCreatedConsumer>();
            x.AddConsumer<BlogDeletedConsumer>();
            
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqSettings["Host"], h =>
                {
                    h.Username(rabbitMqSettings["Username"]);
                    h.Password(rabbitMqSettings["Password"]);
                });
                
                cfg.ReceiveEndpoint("blog-queue", e =>
                {
                    e.ConfigureConsumer<BlogCreatedConsumer>(context);
                    e.ConfigureConsumer<BlogDeletedConsumer>(context);
                });
            });
        });
        
        return services;
    }

    private static IServiceCollection HangfireConfigure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("HangfireConnection");
        services.AddHangfire(config => 
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connection, new SqlServerStorageOptions()));
        
        services.AddHangfireServer();

        
        return services;
    }
}