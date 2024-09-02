using BusinessLogicLayer.Dtos.User;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("[controller]")]  
public class AccountController(
    IAccountService accountService) 
    : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
    {
        var user = await accountService.RegisterAsync(userRegisterDto);
        return Ok(user);
    }
}