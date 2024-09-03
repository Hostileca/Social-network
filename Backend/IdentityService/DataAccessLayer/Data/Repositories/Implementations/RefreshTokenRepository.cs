using DataAccessLayer.Data.Contexts;
using DataAccessLayer.Data.Repositories.Interfaces;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Data.Repositories.Implementations;

public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(AppDbContext context) : base(context)
    {
    }
}