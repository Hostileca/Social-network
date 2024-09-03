using DataAccessLayer.Data;
using DataAccessLayer.Data.Contexts;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

public static class DbInitializer
{
    public static void Initialize(AppDbContext appDbcontext, PersistedGrantDbContext persistedGrantDbContext)
    {
        persistedGrantDbContext.Database.Migrate();
        appDbcontext.Database.Migrate();
    }
}