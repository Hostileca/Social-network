using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entities;

public class User : IdentityUser
{
    public virtual IEnumerable<RefreshToken> RefreshTokens { get; set; }
}