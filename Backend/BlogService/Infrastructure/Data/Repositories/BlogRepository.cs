using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class BlogRepository(
    MongoDbContext context) 
    : RepositoryBase<Blog>(context), IBlogRepository;