using BusinessLogicLayer.Dtos.User;
using DataAccessLayer.Entities;
using Mapster;

namespace BusinessLogicLayer.MappingProfiles;

public class UserMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserReadDto>()
            .Map(dest => dest.Username, src => src.UserName);
        
        config.NewConfig<UserRegisterDto, User>()
            .Map(dest => dest.UserName, src => src.Username)
            .Map(dest => dest.Email, src => src.Email);
    }
}