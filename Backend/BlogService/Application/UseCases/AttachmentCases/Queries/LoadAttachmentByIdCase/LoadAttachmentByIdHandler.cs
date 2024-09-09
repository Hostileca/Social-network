using Application.Dtos;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using MediatR;

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
            throw new NotFoundException(typeof(Attachment).ToString());
        }
        
        var file = await attachmentRepository.LoadAsync(attachment.FilePath, cancellationToken);
        
        return new AttachmentReadDto
        {
            File = file,
            ContentType = attachment.ContentType
        };
    }
}