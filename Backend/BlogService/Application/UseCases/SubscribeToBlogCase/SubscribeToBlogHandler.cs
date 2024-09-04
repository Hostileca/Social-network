using Application.Dtos;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.SubscribeToBlogCase;

public class SubscribeToBlogHandler(
    IBlogRepository repository) 
    : IRequestHandler<SubscribeToBlogCommand, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(SubscribeToBlogCommand request, CancellationToken cancellationToken)
    {
        var userBlog = await repository.GetByIdAsync(request.UserBlogId, cancellationToken);
        
        var blog = await repository.GetByIdAsync(request.BlogId, cancellationToken);

        userBlog.Subscribtions.Add(new Subscriber
        {
            SubscribedAt = blog,
            Blog = userBlog
        });

        await repository.SaveChangesAsync(cancellationToken);
        
        return userBlog.Subscribtions.Adapt<IEnumerable<BlogReadDto>>();
    }
}