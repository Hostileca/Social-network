using Domain.Entities;
using Mapster;
using SharedResources.Dtos;

namespace Application.Mapping;

public class AttachmentMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Attachment, AttachmentReadDto>()
            .Map(dest => dest.ContentType, src => src.ContentType)
            .Map(dest => dest.File, 
                src => Convert.FromBase64String(src.Data));
    }
}