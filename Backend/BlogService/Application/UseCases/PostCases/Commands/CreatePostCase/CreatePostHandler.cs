using Application.Dtos;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;
using Mapster;
using MediatR;

namespace Application.UseCases.PostCases.Commands.CreatePostCase;

public class CreatePostHandler(
    IPostRepository postRepository,
    IBlogRepository blogRepository,
    IAttachmentRepository attachmentRepository)
    : IRequestHandler<CreatePostCommand, PostReadDto>
{
    public async Task<PostReadDto> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken);

        if (blog is null)
        {
            throw new NotFoundException(typeof(Blog).ToString());
        }

        if (blog.UserId != request.UserId)
        {
            throw new UnauthorizedException("It is not your blog");
        }
        
        var post = request.Adapt<Post>();
        
        await postRepository.AddAsync(post, cancellationToken);
        
        if(request.Attachments is not null)
        {
            foreach (var file in request.Attachments)
            {
                await attachmentRepository.AddAsync(
                    new Attachment{Id = Guid.NewGuid().ToString(), File = file, Post = post}, 
                    cancellationToken);
            }
        }

        await postRepository.SaveChangesAsync(cancellationToken);
        
        return post.Adapt<PostReadDto>();
    }
}