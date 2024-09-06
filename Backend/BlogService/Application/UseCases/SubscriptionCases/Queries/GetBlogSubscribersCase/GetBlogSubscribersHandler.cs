using Application.Dtos;
using Application.Repositories;
using Application.Specifications.Common;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscribersCase;

public class GetBlogSubscribersHandler(
    IBlogRepository blogRepository) 
    : IRequestHandler<GetBlogSubscribersQuery, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(GetBlogSubscribersQuery request, CancellationToken cancellationToken)
    {
        var specification = new BlogSubscribersSpecification(request.BlogId);
        var blogs = blogRepository.Find(specification);
        return blogs.Adapt<IEnumerable<BlogReadDto>>();
    }
}