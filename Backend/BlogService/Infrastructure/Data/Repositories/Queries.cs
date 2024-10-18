using Domain.Filters;

namespace Infrastructure.Data.Repositories;

public static class Queries
{
    public static IQueryable<TEntity> Paged<TEntity>(this IQueryable<TEntity> query, PagedFilter pagedFilter)
    {
        return query
            .Skip((pagedFilter.PageNumber - 1) * pagedFilter.PageSize)
            .Take(pagedFilter.PageSize);
    }
}