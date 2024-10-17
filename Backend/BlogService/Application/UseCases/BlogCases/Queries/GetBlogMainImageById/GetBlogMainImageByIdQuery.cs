using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.UseCases.BlogCases.Queries.GetBlogMainImageById;

public class GetBlogMainImageByIdQuery : IRequest<byte[]>
{
    [JsonIgnore]
    [BindNever]
    public string? BlogId { get; set; }    
}