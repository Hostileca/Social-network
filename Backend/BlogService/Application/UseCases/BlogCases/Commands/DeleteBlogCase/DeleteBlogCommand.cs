using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Commands.DeleteBlogCase;

public class DeleteBlogCommand : IRequest<BlogReadDto>
{
    [BindNever]
    public string? UserBlogId { get; set; }    
    
    [BindNever]
    public string? UserId { get; set; }
}