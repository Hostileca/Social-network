using DataAccessLayer;
using DataAccessLayer.Data;

namespace IdentityService;

public static class PresentationLayerInjection
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        services.LoggerConfigure();
        services.AddEndpointsApiExplorer();
        services.AddControllersWithViews();
        return services;
    }
    
    
    private static IServiceCollection LoggerConfigure(this IServiceCollection services)
    {
        //services.AddLogging();

        return services;
    }
    
    public static WebApplication StartApplication(this WebApplication webApplication)
    {
        webApplication.DbInitialize();
        webApplication.UseIdentityServer();
        webApplication.UseHttpsRedirection();
        webApplication.MapDefaultControllerRoute();
        webApplication.MapControllers();
        webApplication.UseAuthentication();
        webApplication.UseAuthorization();
        webApplication.Run();
         
        return webApplication;
    }
    
    private static WebApplication DbInitialize(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        try
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
        }

        return webApplication;
    }
}