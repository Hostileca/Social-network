using Application.Helpers;
using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using SharedResources.Dtos;

namespace Application.MappingConfigurations;

public class AttachmentMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Attachment, AttachmentReadDto>()
            .Map(dest => dest.FileName, src => src.FileName)
            .Map(dest => dest.ContentType, src => src.ContentType)
            .Map(dest => dest.File, 
                src => Convert.FromBase64String(src.Data));
        
        config.NewConfig<IFormFile, Attachment>()
            .Map(dest => dest.Id, src => Guid.NewGuid())
            .Map(dest => dest.Data, src => Base64Converter.ConvertToBase64(src))
            .Map(dest => dest.ContentType, src => MimeTypes.GetMimeType(src.FileName))
            .Map(dest => dest.FileName, src => src.FileName);
    }

}