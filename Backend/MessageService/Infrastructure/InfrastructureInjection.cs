﻿using Application.SignalR.Services;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Infrastructure.SignalR.Services;
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
            .UseSqlServer(sqlConnectionBuilder.ConnectionString));
        
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
        
        return services;
    }
}