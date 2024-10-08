using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.LikeCases.Commands.RemoveLikeFromPostCase;

public class RemoveLikeFromPostCommand : IRequest<PostLikesReadDto>
{
    [FromRoute]
    public string? PostId { get; set; }
    
    [BindNever]
    public string? UserId { get; set; }
    
    public string BlogId { get; set; }
}