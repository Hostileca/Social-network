using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MassTransit;
using SharedResources.MessageBroker.Events;

namespace Infrastructure.MesssageBroker.Consumers;

public class BlogUpdatedConsumer(IBlogRepository blogRepository) : IConsumer<BlogUpdatedEvent>
{
    public async Task Consume(ConsumeContext<BlogUpdatedEvent> context)
    {
        var blogUpdatedEvent = context.Message;
        var existingBlog = await blogRepository.GetByIdAsync(blogUpdatedEvent.Id);
        existingBlog = blogUpdatedEvent.Adapt<Blog>();

        await blogRepository.SaveChangesAsync();
    }
}