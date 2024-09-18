using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.CommentCases.Queries.GetPostCommentsCase;

public class GetPostCommentsQuery : IRequest<IEnumerable<CommentReadDto>>
{
    public string PostId { get; set; }
}