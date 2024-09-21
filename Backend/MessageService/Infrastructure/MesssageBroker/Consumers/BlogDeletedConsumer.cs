using Domain.Repositories;
using MassTransit;
using SharedResources.MessageBroker.Events;

namespace Infrastructure.MesssageBroker.Consumers;

public class BlogDeletedConsumer(IBlogRepository blogRepository) : IConsumer<BlogDeletedEvent>
{
    public async Task Consume(ConsumeContext<BlogDeletedEvent> context)
    {
        var blogDeletedEvent = context.Message;
        
        var blog = await blogRepository.GetByIdAsync(blogDeletedEvent.Id);
        
        blogRepository.Delete(blog);

        await blogRepository.SaveChangesAsync();
    }
}