using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.ReactionCases.Commands.SendReaction;

public class SendReactionCommand : IRequest<ReactionReadDto>
{
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; }
    
    [BindNever]
    [JsonIgnore]
    public Guid MessageId { get; set; }
    
    [BindNever]
    [JsonIgnore]
    public Guid UserBlogId { get; set; }
    
    public string Emoji { get; set; }
}