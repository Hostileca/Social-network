using Application.Dtos;
using MediatR;

namespace Application.UseCases.BlogCases.Queries.GetUserBlogsCase;

public class GetUserBlogsQuery : IRequest<UserBlogsDto>
{
    public string UserId { get; set; }
}