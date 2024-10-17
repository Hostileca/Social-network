using Application.UseCases.BaseQueries.Paged;
using Domain.Filters;
using Mapster;

namespace Application.Mapping;

public class FilterMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PagedQuery, PagedFilter>()
            .Map(dest => dest.PageNumber, src => src.PageNumber)
            .Map(dest => dest.PageSize, src => src.PageSize);
    }
}