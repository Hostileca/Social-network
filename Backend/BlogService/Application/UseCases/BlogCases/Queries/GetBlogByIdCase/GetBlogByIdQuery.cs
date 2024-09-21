using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetBlogByIdCase;

public class GetBlogByIdQuery: IRequest<BlogReadDto>
{
    public string? BlogId { get; set; }
}