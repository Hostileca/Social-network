﻿namespace Application.Dtos;

public class BlogLikesReadDto
{
    public IEnumerable<PostReadDto> Posts { get; set; }
}