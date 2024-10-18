using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MassTransit;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;
using SharedResources.MessageBroker.Events;

namespace Application.UseCases.BlogCases.Commands.DeleteBlogCase;

public class DeleteBlogHandler(
    IBlogRepository blogRepository,
    ICacheRepository cacheRepository,
    IPublishEndpoint publishEndpoint) 
    : IRequestHandler<DeleteBlogCommand, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
    {
        var existingBlog = await blogRepository.GetByIdAndUserIdAsync(request.UserBlogId, request.UserId, cancellationToken);
        
        if (existingBlog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId);
        }
        
        blogRepository.Delete(existingBlog);

        await blogRepository.SaveChangesAsync(cancellationToken);

        var blogReadDto = existingBlog.Adapt<BlogReadDto>();
        var blogDeletedEvent = blogReadDto.Adapt<BlogDeletedEvent>();

        await publishEndpoint.Publish(blogDeletedEvent, cancellationToken);
        
        await cacheRepository.DeleteAsync<BlogReadDto>(blogReadDto.Id.ToString());
        
        return blogReadDto;
    }
}