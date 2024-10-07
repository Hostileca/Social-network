using Application.Configs;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MassTransit;
using MediatR;
using SharedResources.Consts;
using SharedResources.Dtos;
using SharedResources.Exceptions;
using SharedResources.MessageBroker.Events;

namespace Application.UseCases.BlogCases.Commands.UpdateBlogCase;

public class UpdateBlogHandler(
    IBlogRepository blogRepository,
    IAttachmentRepository attachmentRepository,
    ICacheRepository cacheRepository,
    IPublishEndpoint publishEndpoint)
    : IRequestHandler<UpdateBlogCommand, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
    {
        var existingBlog = await blogRepository.GetByIdAndUserIdAsync(request.BlogId, request.UserId ,cancellationToken);
        
        if (existingBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }
        
        existingBlog = request.Adapt<Blog>();
        
        var existingImageAttachment = await attachmentRepository.GetAttachmentByIdAndType(
            request.ImageAttachmentId, AttachmentSimpleTypes.Image, cancellationToken);

        if (existingImageAttachment is null)
        {
            throw new NotFoundException(typeof(Attachment).ToString(), request.ImageAttachmentId);
        }
        
        existingBlog.ImageAttachmentId = existingImageAttachment.Id;

        await blogRepository.SaveChangesAsync(cancellationToken);

        var blogReadDto = existingBlog.Adapt<BlogReadDto>();
        
        var blogUpdatedEvent = blogReadDto.Adapt<BlogUpdatedEvent>();
        
        await publishEndpoint.Publish(blogUpdatedEvent, cancellationToken);
        
        await cacheRepository.SetAsync(blogReadDto.Id.ToString(), blogReadDto, CacheConfig.BlogCacheTime);
        
        return blogReadDto;
    }
}