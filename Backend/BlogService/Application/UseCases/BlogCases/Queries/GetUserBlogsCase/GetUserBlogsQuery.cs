using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetUserBlogsCase;

public class GetUserBlogsQuery : IRequest<UserBlogsDto>
{
    public string UserId { get; set; }
}