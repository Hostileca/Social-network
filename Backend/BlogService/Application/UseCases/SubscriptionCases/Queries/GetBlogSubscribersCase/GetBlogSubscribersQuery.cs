using System.Text.Json.Serialization;
using Application.UseCases.Base.Queries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscribersCase;

public class GetBlogSubscribersQuery : PagedQuery, IRequest<IEnumerable<SubscriberReadDto>>
{
    [BindNever]
    public string? BlogId { get; set; }
}