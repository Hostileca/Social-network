using Application.UseCases.Base.Queries.Paged;
using Application.UseCases.BlogCases.Queries.GetBlogsByFilterCase;
using Domain.Filters;
using Mapster;

namespace Application.MappingConfigurations;

public class FilterMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PagedQuery, PagedFilter>()
            .Map(dest => dest.PageNumber, src => src.PageNumber)
            .Map(dest => dest.PageSize, src => src.PageSize);
        
        config.NewConfig<GetBlogsByFilterQuery, BlogFilter>()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.MinimalAge, src => src.MinimalAge)
            .Map(dest => dest.MaximumAge, src => src.MaximumAge)
            .Map(dest => dest.MinimalSubscribersCount, src => src.MinimalSubscribersCount)
            .Map(dest => dest.MaximumSubscribersCount, src => src.MaximumSubscribersCount)
            .Map(dest => dest.MinimalPostsCount, src => src.MinimalPostsCount)
            .Map(dest => dest.MaximumPostsCount, src => src.MaximumPostsCount);
    }
}