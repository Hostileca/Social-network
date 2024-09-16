using System.Text;
using Application.Dtos;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigurations;

public class AttachmentMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Attachment, AttachmentReadDto>()
            .Map(dest => dest.ContentType, src => src.ContentType)
            .Map(dest => dest.File, 
                src => Convert.FromBase64String(src.Data));
    }
}