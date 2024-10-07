using Domain.Filters;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetBlogsByFilterCase;

public class GetBlogsByFilterHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogsByFilterQuery, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(GetBlogsByFilterQuery request, CancellationToken cancellationToken)
    {
        var pagedFilter = request.Adapt<PagedFilter>();
        var blogFilter = request.Adapt<BlogFilter>();

        var blogs = await blogRepository.GetBlogsByFilter(pagedFilter, blogFilter, cancellationToken);

        return blogs.Adapt<IEnumerable<BlogReadDto>>();
    }
}