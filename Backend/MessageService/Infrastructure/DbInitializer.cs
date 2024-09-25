using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DbInitializer
{
    public static void Initialize(AppDbContext appDbcontext)
    {
        appDbcontext.Database.Migrate();
    }
}