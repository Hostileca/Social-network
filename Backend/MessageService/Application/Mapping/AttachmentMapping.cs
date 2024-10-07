using Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.Http;
using SharedResources.Dtos;

namespace Application.Mapping;

public class AttachmentMapping : IRegister
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
            .Map(dest => dest.Data, src => ConvertToBase64Async(src))
            .Map(dest => dest.ContentType, src => MimeTypes.GetMimeType(src.FileName))
            .Map(dest => dest.FileName, src => src.FileName);
    }
    
    private static string ConvertToBase64Async(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
        {
            return string.Empty;
        }

        using var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);
        var fileBytes = memoryStream.ToArray();

        return Convert.ToBase64String(fileBytes);
    }
}