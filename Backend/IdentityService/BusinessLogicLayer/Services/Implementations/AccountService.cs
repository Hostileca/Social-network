using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLogicLayer.Dtos.Tokens;
using BusinessLogicLayer.Dtos.User;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Algorithms;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Repositories.Interfaces;
using DataAccessLayer.Entities;
using IdentityModel.Client;
using IdentityServer4.EntityFramework.Stores;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RefreshToken = DataAccessLayer.Entities.RefreshToken;
using Token = BusinessLogicLayer.Dtos.Tokens.Token;

namespace BusinessLogicLayer.Services.Implementations;

public class AccountService(
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IRefreshTokenRepository refreshTokenRepository,
    TokensGenerator tokensGenerator) 
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
        
        var createUserResult = await userManager.CreateAsync(newUser, userRegisterDto.Password);
        var addToRoleResult = await userManager.AddToRoleAsync(newUser, Roles.User);

        
        if (!createUserResult.Succeeded)
        {
            throw new CreationException(createUserResult.Errors.ToString());
        }

        if (!addToRoleResult.Succeeded)
        {
            throw new CreationException(addToRoleResult.Errors.ToString());
        }
        
        return newUser.Adapt<UserReadDto>();
    }

    public async Task<TokensResponse> RefreshTokenAsync(TokenRefreshRequest tokenRefreshRequest)
    {
        var currentRefreshToken = await refreshTokenRepository.GetByIdAsync(
            new Guid(tokenRefreshRequest.RefreshToken));
        
        if (currentRefreshToken is null)
        {
            throw new NotFoundException(typeof(RefreshToken).ToString());
        }

        var user = currentRefreshToken.User;
        refreshTokenRepository.Delete(currentRefreshToken);
        if (!currentRefreshToken.IsActive)
        {
            throw new ExpireException(typeof(RefreshToken).ToString());
        }

        return await GenerateAndSaveTokens(user);
    }

    public async Task<UserReadDto> UpdateAsync(UserUpdateDto userUpdateDto)
    {
        var user = await userManager.FindByIdAsync(userUpdateDto.Id);
    
        if (user is null)
        {
            throw new NotFoundException(typeof(User).ToString());
        }
        
        userUpdateDto.Adapt(user);

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            throw new UpdateException(typeof(User).ToString());
        }

        return user.Adapt<UserReadDto>();
    }

    public async Task<UserReadDto> DeleteAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
    
        if (user is null)
        {
            throw new NotFoundException(typeof(User).ToString());
        }
        
        var result = await userManager.DeleteAsync(user);
        
        if (!result.Succeeded)
        {
            throw new DeleteException(typeof(User).ToString());
        }
        
        return user.Adapt<UserReadDto>();
    }

    public async Task<UserReadDto> GetUserById(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
    
        if (user is null)
        {
            throw new NotFoundException(typeof(User).ToString());
        }
        
        return user.Adapt<UserReadDto>();
    }

    public async Task<TokensResponse> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await userManager.FindByEmailAsync(userLoginDto.Email);
        
        if (user is null)
        {
            throw new NotFoundException(typeof(User).ToString());
        }
        
        var result = await userManager.CheckPasswordAsync(user, userLoginDto.Password);
        
        if (!result)
        {
            throw new UnauthorizedException("Wrong email or password");
        }

        return await GenerateAndSaveTokens(user);
    }

    private async Task<TokensResponse> GenerateAndSaveTokens(User user)
    {
        var refreshToken = tokensGenerator.GenerateRefreshToken();
        var refreshTokenEntity = refreshToken.Adapt<RefreshToken>();
        refreshTokenEntity.User = user;
        
        await refreshTokenRepository.AddAsync(refreshTokenEntity);
        
        await refreshTokenRepository.SaveChangesAsync();
        
        var tokenResponse = new TokensResponse
        {
            AccessToken = tokensGenerator.GenerateAccessToken(refreshTokenEntity.User, 
                await userManager.GetRolesAsync(refreshTokenEntity.User)),
            RefreshToken = refreshToken
        };
        
        return tokenResponse;
    }
}