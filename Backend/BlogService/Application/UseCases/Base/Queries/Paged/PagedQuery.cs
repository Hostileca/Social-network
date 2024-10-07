using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases.Base.Queries.Paged;

public abstract class PagedQuery
{
    [FromQuery]
    public int PageNumber { get; set; }
    
    [FromQuery]
    public int PageSize { get; set; }
}