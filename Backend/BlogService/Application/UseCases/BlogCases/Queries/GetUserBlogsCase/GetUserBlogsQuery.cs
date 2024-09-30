using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetUserBlogsCase;

public class GetUserBlogsQuery : IRequest<IEnumerable<BlogReadDto>>
{
    public string UserId { get; set; }
}