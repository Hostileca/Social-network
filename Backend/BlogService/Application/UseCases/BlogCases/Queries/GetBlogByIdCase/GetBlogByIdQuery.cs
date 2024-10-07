using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Queries.GetBlogByIdCase;

public class GetBlogByIdQuery: IRequest<BlogReadDto>
{
    [FromRoute]
    public string? BlogId { get; set; }
}