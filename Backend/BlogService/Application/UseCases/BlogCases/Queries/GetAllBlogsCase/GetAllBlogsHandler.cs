using Domain.Filters;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetAllBlogsCase;

public class GetAllBlogsHandler(
    IBlogRepository blogRepository) 
    : IRequestHandler<GetAllBlogsQuery, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
    {
        var pagedFilter = request.Adapt<PagedFilter>();
        
        var blogs = await blogRepository.GetAllAsync(pagedFilter, cancellationToken);
        
        return blogs.Adapt<IEnumerable<BlogReadDto>>();
    }
}