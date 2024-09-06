using Microsoft.AspNetCore.Authorization;

namespace PresentationLayer.Policies;

public class RolesRequirement : IAuthorizationRequirement
{
    public List<string> AllowedRoles { get; }
    
    public RolesRequirement(List<string> allowedRoles)
    {
        AllowedRoles = allowedRoles;
    }
}