using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedResources.Dtos;

namespace Application.UseCases.AttachmentCases.Queries.LoadAttachmentByIdCase;

public class LoadAttachmentByIdQuery : IRequest<AttachmentReadDto>
{
    [FromRoute]
    public string AttachmentId { get; set; }
}