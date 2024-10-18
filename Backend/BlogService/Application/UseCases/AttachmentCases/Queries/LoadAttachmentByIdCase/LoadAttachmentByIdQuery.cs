using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SharedResources.Dtos;

namespace Application.UseCases.AttachmentCases.Queries.LoadAttachmentByIdCase;

public class LoadAttachmentByIdQuery : IRequest<AttachmentReadDto>
{
    [BindNever]
    public string? AttachmentId { get; set; }
}