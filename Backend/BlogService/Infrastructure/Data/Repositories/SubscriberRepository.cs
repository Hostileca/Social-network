using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class SubscriberRepository(
    MongoDbContext context) 
    : RepositoryBase<Subscriber>(context), ISubscriberRepository;