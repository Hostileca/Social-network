using BusinessLogicLayer.Dtos.User;
using BusinessLogicLayer.Exceptions;
using DataAccessLayer.Models;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services.Implementations;

public class AccountService(
    SignInManager<User> signInManager,
    UserManager<User> userManager) 
    : IAccountService
{
    public async Task<UserReadDto> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        var user = await userManager.FindByEmailAsync(userRegisterDto.Email);
        if (user is not null)
        {
            throw new DuplicateException(typeof(User).ToString());
        }
        
        var newUser = new User { UserName = userRegisterDto.Username, Email = userRegisterDto.Email };
        var result = await userManager.CreateAsync(newUser, userRegisterDto.Password);
        
        if (!result.Succeeded)
        {
            throw new AuthorizationException("User creation failed");
        }

        return new UserReadDto();
    }
}