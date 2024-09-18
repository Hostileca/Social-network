using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MassTransit;
using MediatR;
using SharedResources.Dtos;
using SharedResources.MessageBroker.Events;

namespace Application.UseCases.BlogCases.Commands.CreateBlogCase;

public class CreateBlogHandler(
    IBlogRepository repository,
    IPublishEndpoint publishEndpoint) 
    : IRequestHandler<CreateBlogCommand, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var newBlog = request.Adapt<Blog>();
        
        await repository.AddAsync(newBlog, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        var blogReadDto = newBlog.Adapt<BlogReadDto>();
        var blogCreatedEvent = blogReadDto.Adapt<BlogCreatedEvent>();
        
        await publishEndpoint.Publish(blogCreatedEvent, cancellationToken);
        
        return blogReadDto;
    }
}