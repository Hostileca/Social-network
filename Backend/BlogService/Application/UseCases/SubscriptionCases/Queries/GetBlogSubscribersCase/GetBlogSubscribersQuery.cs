using System.Text.Json.Serialization;
using Application.UseCases.Base.Queries.Paged;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedResources;
using SharedResources.Dtos;

namespace Application.UseCases.SubscriptionCases.Queries.GetBlogSubscribersCase;

public class GetBlogSubscribersQuery : PagedQuery, IRequest<IEnumerable<SubscriberReadDto>>
{
    [FromRoute]
    public string? BlogId { get; set; }
}