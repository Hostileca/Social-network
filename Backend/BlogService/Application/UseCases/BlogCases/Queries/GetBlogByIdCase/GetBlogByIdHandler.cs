using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.BlogCases.Queries.GetBlogByIdCase;

public class GetBlogByIdHandler(
    IBlogRepository blogRepository) 
    : IRequestHandler<GetBlogByIdQuery, BlogReadDto>
{
    public async Task<BlogReadDto> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);
        
        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.BlogId);
        }
        
        return blog.Adapt<BlogReadDto>();
    }
}