using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Data.Repositories;

public class BlogRepository(
    AppDbContext context) 
    : RepositoryBase<Blog>(context), IBlogRepository;