using Application.Dtos;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

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
            throw new NotFoundException(typeof(Blog).ToString());
        }
        
        return blog.Adapt<BlogReadDto>();
    }
}