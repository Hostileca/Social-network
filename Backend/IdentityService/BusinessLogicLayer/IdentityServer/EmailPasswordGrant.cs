using BusinessLogicLayer.Entities;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.IdentityServer;

public class EmailPasswordGrant(UserManager<User> userManager, SignInManager<User> signInManager)
    : IExtensionGrantValidator
{
    public async Task ValidateAsync(ExtensionGrantValidationContext context)
    {
        var email = context.Request.Raw.Get("email");
        var password = context.Request.Raw.Get("password");

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid email or password.");
            return;
        }

        var user = await userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var result = await signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                context.Result = new GrantValidationResult(
                    user.Id.ToString(),
                    GrantType,
                    customResponse: new Dictionary<string, object> { { "email", user.Email } });
                return;
            }
        }

        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid email or password.");
    }

    public string GrantType => IdentityServer.GrantType.EmailPasswordGrantType;
}