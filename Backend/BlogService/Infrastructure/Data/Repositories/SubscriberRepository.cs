using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class SubscriberRepository(
    MongoDbContext context) 
    : RepositoryBase<Subscription>(context), ISubscriberRepository;