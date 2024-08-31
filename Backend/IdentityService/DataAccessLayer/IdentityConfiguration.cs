using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace DataAccessLayer;

public static class IdentityConfiguration
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("GatewayAccess"),
            new ApiScope(IdentityServerConstants.StandardScopes.OpenId),
            new ApiScope(IdentityServerConstants.StandardScopes.OfflineAccess),
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiResource> ApiResources = new List<ApiResource>();

    public static IEnumerable<Client> Clients = new List<Client>
    {
        new Client
        {
            ClientId = "test",
            ClientSecrets = { new Secret("testSecret".ToSha256()) },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            RequirePkce = false,
            RedirectUris = { "https://test/"},
            PostLogoutRedirectUris = { "https://test/"},
            AllowedScopes = 
            { 
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.OfflineAccess,
            },
            RequireConsent = false,
            AlwaysIncludeUserClaimsInIdToken = true,
            AllowOfflineAccess = true,
        }
    };
}