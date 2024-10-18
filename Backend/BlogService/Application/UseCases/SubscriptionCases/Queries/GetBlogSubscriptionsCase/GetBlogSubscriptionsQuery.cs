using Application.UseCases.Base.Queries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscriptionsCase;

public class GetBlogSubscriptionsQuery : PagedQuery, IRequest<IEnumerable<SubscriptionReadDto>>
{
    [BindNever]
    public string? BlogId { get; set; }
}