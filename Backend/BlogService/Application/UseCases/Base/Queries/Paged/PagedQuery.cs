using Microsoft.AspNetCore.Mvc;

namespace Application.UseCases.Base.Queries.Paged;

public abstract class PagedQuery
{
    public int PageNumber { get; set; }
    
    public int PageSize { get; set; }
}