using Application.Commands.Blog;
using Application.Dtos;
using Domain.Entities;
using Mapster;

namespace Application.MappingProfiles;

public class BlogMappingProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Blog, BlogReadDto>();
        config.NewConfig<CreateBlogCommand, Blog>();
    }
}