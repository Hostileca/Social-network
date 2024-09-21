using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.LikeCases.Queries.GetBlogLikesCase;

public class GetBlogLikesQuery : IRequest<BlogLikesReadDto>
{
    public string BlogId { get; set; }
}