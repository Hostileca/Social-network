using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Data.Repositories;

public class BlogRepository(
    MongoDbContext context) 
    : RepositoryBase<Blog>(context), IBlogRepository;