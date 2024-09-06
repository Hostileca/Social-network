using Application.Dtos;
using Application.Repositories;
using Application.Specifications.Common;
using Mapster;
using MediatR;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscriptionsCase;

public class GetBlogSubscriptionsHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetBlogSubscriptionsQuery, IEnumerable<BlogReadDto>>
{
    public async Task<IEnumerable<BlogReadDto>> Handle(GetBlogSubscriptionsQuery request,
        CancellationToken cancellationToken)
    {
        var specification = new BlogSubscriptionsSpecification(request.BlogId);
        var blogs = blogRepository.Find(specification);
        return blogs.Adapt<IEnumerable<BlogReadDto>>();
    }
}