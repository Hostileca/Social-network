using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.AttachmentCases.Queries.LoadAttachmentByIdCase;

public class LoadAttachmentByIdHandler(
    IAttachmentRepository attachmentRepository)
    : IRequestHandler<LoadAttachmentByIdQuery, AttachmentReadDto>
{
    public async Task<AttachmentReadDto> Handle(LoadAttachmentByIdQuery request, CancellationToken cancellationToken)
    {
        var attachment = await attachmentRepository.GetByIdAsync(request.Id, cancellationToken);

        if (attachment is null)
        {
            throw new NotFoundException(typeof(Attachment).ToString(), request.Id);
        }

        return attachment.Adapt<AttachmentReadDto>();
    }
}