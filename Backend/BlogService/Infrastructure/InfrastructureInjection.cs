using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using UserGrpc;

namespace Infrastructure;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.DbConfigure(configuration);
        services.RepositoriesConfigure();
        services.MessageBrokerConfigure(configuration);
        services.GrpcConfigure(configuration);
        services.RedisConfigure(configuration);

        return services;
    }
    
    private static IServiceCollection DbConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDbConnection");
        
        services.AddDbContext<MongoDbContext>(options => options
            .UseMongoDB(connectionString, "BlogServiceDb")
            .UseLazyLoadingProxies());
        return services;
    }
    
    private static IServiceCollection RepositoriesConfigure(this IServiceCollection services)
    {
        services.AddScoped<IAttachmentRepository, AttachmentRepository>();
        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ISubscriberRepository, SubscriberRepository>();
        
        return services;
    }
    
    private static IServiceCollection MessageBrokerConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqSettings = configuration.GetSection("RabbitMqSettings");
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqSettings["Host"], h =>
                {
                    h.Username(rabbitMqSettings["Username"]);
                    h.Password(rabbitMqSettings["Password"]);
                });
            });
        });
        
        return services;
    }

    private static IServiceCollection GrpcConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("UserGrpcConnection");
        services.AddGrpcClient<UserGrpcService.UserGrpcServiceClient>(options =>
        {
            options.Address = new Uri(connectionString);
        });
        
        return services;
    }

    private static IServiceCollection RedisConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetConnectionString("RedisConnection");
        var redis = ConnectionMultiplexer.Connect(redisConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(redis);
        services.AddScoped<ICacheRepository, CacheRepository>();
        
        return services;
    }
}