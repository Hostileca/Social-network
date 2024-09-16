﻿using Application.Dtos;
using Application.UseCases.BlogCases.Commands.CreateBlogCase;
using Domain.Entities;
using Mapster;

namespace Application.MappingConfigurations;

public class BlogMappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateBlogCommand, Blog>()
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Id, src => Guid.NewGuid().ToString());

        config.NewConfig<Blog, BlogReadDto>();
    }
}