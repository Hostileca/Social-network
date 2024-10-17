using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MediatR;
using SharedResources.Dtos;
using SharedResources.Exceptions;

namespace Application.UseCases.AttachmentCases.Queries.GetAttachmentById;

public class GetAttachmentByIdHandler(
    IBlogRepository blogRepository)
    : IRequestHandler<GetAttachmentByIdQuery, AttachmentReadDto>
{
    public async Task<AttachmentReadDto> Handle(GetAttachmentByIdQuery request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetBlogByIdAndUserIdAsync(request.UserBlogId, request.UserId,
            cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString(), request.UserBlogId.ToString());
        }

        var chatMember = blog.ChatsMember.FirstOrDefault(cm => cm.ChatId == request.ChatId);

        if (chatMember is null)
        {
            throw new NotFoundException(typeof(Chat).ToString(), request.ChatId.ToString());
        }

        var message = chatMember.Chat.Messages.FirstOrDefault(m => m.Id == request.MessageId);

        if (message is null)
        {
            throw new NotFoundException(typeof(Message).ToString(), request.MessageId.ToString());
        }

        var attachment = message.Attachments.FirstOrDefault(at => at.Id == request.AttachmentId);

        if (attachment is null)
        {
            throw new NotFoundException(typeof(Attachment).ToString(), request.AttachmentId.ToString());
        }

        return attachment.Adapt<AttachmentReadDto>();
    }
}