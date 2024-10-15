using Application.Helpers;
using Application.UseCases.BlogCases.Commands.CreateBlogCase;
using Application.UseCases.BlogCases.Commands.UpdateBlogCase;
using Domain.Entities;
using Mapster;
using SharedResources.Consts;
using SharedResources.Dtos;
using SharedResources.MessageBroker.Events;

namespace Application.MappingConfigurations;

public class BlogMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBlogCommand, Blog>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString());

        config.NewConfig<Blog, BlogReadDto>()            
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.Bio, src => src.Bio ?? "")
            .Map(dest=> dest.DateOfBirth, src => src.DateOfBirth)
            .Map(dest => dest.PostsCount, src => src.Posts != null? src.Posts.Count() : 0)
            .Map(dest => dest.SubscribersCount, src => src.Subscribers != null? src.Subscribers.Count() : 0)
            .Map(dest => dest.SubscriptionsCount, src => src.Subscriptions != null ? src.Subscriptions.Count() : 0);

        config.NewConfig<Blog, BlogCreatedEvent>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.UserId, src => src.UserId);

        config.NewConfig<UpdateBlogCommand, Blog>()
            .Ignore(dest => dest.Id)
            .Ignore(dest => dest.UserId)
            .Map(dest => dest.MainImage, src => Base64Converter.ConvertToBase64(src.MainImage), 
                    src => src.MainImage != null && 
                           MimeTypes.GetMimeType(src.MainImage.FileName).Contains(AttachmentSimpleTypes.Image))
            .Map(dest => dest.DateOfBirth, src => src.DateOfBirth, 
                src=> src.DateOfBirth != DateTime.MinValue)
            .Map(dest => dest.Username, src => src.Username, 
                src => src.Username != null)
            .Map(dest => dest.Bio, src => src.Bio, 
                src => src.Bio != null);
    }
}