using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers;

public class AuthController(
    IAuthService authService)
    : Controller
{
    
    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) return View(loginViewModel);

        var result = await authService.LoginAsync(loginViewModel);
        if (result.IsSuccess) return Redirect(loginViewModel.ReturnUrl);
        
        ModelState.AddModelError(string.Empty, result.Error.Description);
        return View(loginViewModel);
    }
    
    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        returnUrl = returnUrl ?? Url.Content("https://github.com");
        return View(new RegisterViewModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid) return View(registerViewModel);

        var result = await authService.RegisterAsync(registerViewModel);
        if (result.IsSuccess) return Redirect(registerViewModel.ReturnUrl);
        
        ModelState.AddModelError(string.Empty, result.Error.Description);
        return View(registerViewModel);
    }
    
}