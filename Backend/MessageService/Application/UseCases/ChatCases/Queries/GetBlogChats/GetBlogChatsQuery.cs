﻿using System.Text.Json.Serialization;
using MediatR;
using SharedResources.Dtos;

namespace Application.UseCases.ChatCases.Queries.GetBlogChats;

public class GetBlogChatsQuery : IRequest<IEnumerable<ChatReadDto>>
{
    [JsonIgnore]
    public string? UserId { get; set; }
    
    public Guid UserBlogId { get; set; }
}