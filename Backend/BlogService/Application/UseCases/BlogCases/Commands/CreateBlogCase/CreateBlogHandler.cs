using Application.Configs;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MassTransit;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;
using SharedResources.MessageBroker.Events;
using UserGrpc;

namespace Application.UseCases.BlogCases.Commands.CreateBlogCase;

public class CreateBlogHandler(
    IBlogRepository repository,
    IPublishEndpoint publishEndpoint,
    ICacheRepository cacheRepository,
    UserGrpcService.UserGrpcServiceClient userGrpcClient) 
    : IRequestHandler<CreateBlogCommand, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await userGrpcClient.CheckUserAsync(
            new CheckUserRequest { UserId = request.UserId }, cancellationToken: cancellationToken);

        if (!isUserExist.Exists)
        {
            throw new NotFoundException("User", request.UserId);
        }
        
        var newBlog = request.Adapt<Blog>();
        
        await repository.AddAsync(newBlog, cancellationToken);

        await repository.SaveChangesAsync(cancellationToken);

        var blogReadDto = newBlog.Adapt<BlogReadDto>();
        var blogCreatedEvent = newBlog.Adapt<BlogCreatedEvent>();
        
        await publishEndpoint.Publish(blogCreatedEvent, cancellationToken);
        
        await cacheRepository.SetAsync(blogReadDto.Id.ToString(), blogReadDto, CacheConfig.BlogCacheTime);
        
        return blogReadDto;
    }
}