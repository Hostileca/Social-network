﻿using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.LikeCases.Commands.AddLikeToPostCase;

public class AddLikeToPostCommand : IRequest<PostLikesReadDto>
{
    [BindNever]
    [JsonIgnore]
    public string? PostId { get; set; }
    
    [BindNever]
    [JsonIgnore]
    public string? UserId { get; set; }
    
    public string BlogId { get; set; }
}