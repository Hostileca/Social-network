using BusinessLogicLayer.Dtos.User;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogicLayer.Services.Implementations;

public class AccountService(
    UserManager<User> userManager) 
    : IAccountService
{
    public async Task<UserReadDto> RegisterAsync(UserRegisterDto userRegisterDto)
    {
        var user = await userManager.FindByEmailAsync(userRegisterDto.Email);
        
        if (user is not null)
        {
            throw new AlreadyExistsException(typeof(User).ToString());
        }
        
        var newUser = userRegisterDto.Adapt<User>();
        
        var result = await userManager.CreateAsync(newUser, userRegisterDto.Password);
        
        if (!result.Succeeded)
        {
            throw new CreationException(typeof(User).ToString());
        }

        return newUser.Adapt<UserReadDto>();
    }
}