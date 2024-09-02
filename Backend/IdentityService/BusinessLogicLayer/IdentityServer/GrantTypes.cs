namespace BusinessLogicLayer.IdentityServer;

public class GrantTypes : IdentityServer4.Models.GrantTypes
{
    public static ICollection<string> EmailPassword { get; } = new List<string>
    {
        GrantType.EmailPasswordGrantType
    };
}