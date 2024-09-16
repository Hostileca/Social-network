using Application.Dtos;
using MediatR;

namespace Application.UseCases.AttachmentCases.Queries.LoadAttachmentByIdCase;

public class LoadAttachmentByIdQuery : IRequest<AttachmentReadDto>
{
    public string Id { get; set; }
}