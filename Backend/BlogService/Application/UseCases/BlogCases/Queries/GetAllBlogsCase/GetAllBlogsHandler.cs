using Application.Dtos;
using Domain.Repositories;
using Mapster;
using MediatR;

namespace Application.UseCases.BlogCases.Queries.GetAllBlogsCase;

public class GetAllBlogsHandler(
    IBlogRepository blogRepository) 
    : IRequestHandler<GetAllBlogsQuery, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
    {
        var blogs = await blogRepository.GetAllAsync(cancellationToken);
        return blogs.Adapt<IEnumerable<BlogReadDto>>();
    }
}