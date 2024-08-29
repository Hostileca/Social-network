using BusinessLogicLayer.Result;
using BusinessLogicLayer.ViewModels;

namespace BusinessLogicLayer.Services.Interfaces;

public interface IAuthService
{
    Task<Results> RegisterAsync(RegisterViewModel registerViewModel);
    Task<Results> LoginAsync(LoginViewModel loginViewModel);
}