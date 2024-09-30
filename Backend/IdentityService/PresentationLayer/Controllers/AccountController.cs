using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Dtos.Tokens;
using SharedResources.Dtos.User;
using SharedResources.Policies;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("users")]  
public class AccountController(
    IAccountService accountService) 
    : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterUser(UserRegisterDto userRegisterDto)
    {
        var user = await accountService.RegisterAsync(userRegisterDto);
        
        return Ok(user);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        var tokens = await accountService.LoginAsync(userLoginDto);
        
        return Ok(tokens);
    }

    [HttpPost("tokens/refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken(TokenRefreshRequest tokenRefreshRequest)
    {
        var token = await accountService.RefreshTokenAsync(tokenRefreshRequest);
        
        return Ok(token);
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
    {
        var user = await accountService.UpdateAsync(userUpdateDto);

        return Ok(user);
    }
    
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteUser()
    {
        var user = await accountService.DeleteAsync(UserId);
        
        return Ok(user);
    }
    
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetMe()
    {
        var user = await accountService.GetUserById(UserId);
        
        return Ok(user);
    }
    
    [HttpGet("{userId}")]
    [Authorize(Policy = Policies.RequireStaff)]
    public async Task<IActionResult> GetUserById(string userId)
    {
        var user = await accountService.GetUserById(userId);
        
        return Ok(user);
    }
}