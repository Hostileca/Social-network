using Domain.Entities;
using Domain.Repositories;
using Mapster;
using MassTransit;
using SharedResources.MessageBroker.Events;

namespace Infrastructure.MesssageBroker.Consumers;

public class BlogCreatedConsumer(IBlogRepository blogRepository) : IConsumer<BlogCreatedEvent>
{
    public async Task Consume(ConsumeContext<BlogCreatedEvent> context)
    {
        var blogCreatedEvent = context.Message;
        var blog = blogCreatedEvent.Adapt<Blog>(); 
        
        await blogRepository.AddAsync(blog);

        await blogRepository.SaveChangesAsync();
    }
}