using DataAccessLayer.Data;

namespace DataAccessLayer;

public class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();
    }
}