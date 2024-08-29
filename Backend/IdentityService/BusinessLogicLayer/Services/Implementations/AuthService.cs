using BusinessLogicLayer.ViewModels;
using BusinessLogicLayer.Result;
using DataAccessLayer.Models;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services.Implementations;

public class AuthService(
    SignInManager<User> signInManager,
    UserManager<User> userManager) 
    : IAuthService
{
    public async Task<Results> LoginAsync(LoginViewModel loginViewModel)
    {
        var user = await userManager.FindByEmailAsync(loginViewModel.Email);
        if (user == null) return Results.Failure(AuthErrors.IncorrectUserData);

        var result = await signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
        if(!result.Succeeded) return Results.Failure(AuthErrors.IncorrectUserData);

        return Results.Success();
    }

    public async Task<Results> RegisterAsync(RegisterViewModel registerViewModel)
    {
        var user = await userManager.FindByEmailAsync(registerViewModel.Email);
        if (user != null) return Results.Failure(AuthErrors.EmailAlreadyTaken);
        
        var newUser = new User { UserName = registerViewModel.Username, Email = registerViewModel.Email };
        var result = await userManager.CreateAsync(newUser, registerViewModel.Password);
        
        if (!result.Succeeded) return Results.Failure(AuthErrors.UndefinedError);
        
        await signInManager.SignInAsync(newUser, false);
        return Results.Success();
    }
}