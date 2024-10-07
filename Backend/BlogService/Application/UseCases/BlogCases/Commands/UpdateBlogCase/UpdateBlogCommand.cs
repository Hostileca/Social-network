﻿using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.BlogCases.Commands.UpdateBlogCase;

public class UpdateBlogCommand : IRequest<BlogReadDto>
{
    [FromRoute]
    [JsonIgnore]
    public string? BlogId { get; set; }
    
    [JsonIgnore]
    public string? UserId { get; set; }

    public string Username { get; set; }
    
    public string Bio { get; set; }
    
    public DateTime DateOfBirth { get; set; }
    
    public string ImageAttachmentId { get; set; }
}