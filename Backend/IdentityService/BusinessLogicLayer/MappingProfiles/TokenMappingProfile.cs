using DataAccessLayer.Entities;
using Mapster;
using SharedResources.Dtos.Tokens;

namespace BusinessLogicLayer.MappingProfiles;

public class TokenMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Token, RefreshToken>()
            .Map(dest => dest.Key, src => src.Value)
            .Map(dest => dest.ExpirationTime, src => src.ExpiresIn)
            .Map(dest => dest.CreationTime, src => DateTime.UtcNow);
    }
}