using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.PostCases.Commands.DeletePostCase;

public class DeletePostByIdCommand : IRequest<PostReadDto>
{
    [BindNever]
    public string? UserId { get; set; }
    
    [BindNever]
    public string? BlogId { get; set; }
    
    [BindNever]
    public string? PostId { get; set; }
}