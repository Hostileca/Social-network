using BusinessLogicLayer.Dtos.User;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IAccountService
{
    Task<UserReadDto> RegisterAsync(UserRegisterDto userRegisterDto);
    //Task<Results> LoginAsync(LoginViewModel loginViewModel);
}