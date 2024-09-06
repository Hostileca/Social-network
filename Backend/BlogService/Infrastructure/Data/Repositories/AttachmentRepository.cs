using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

namespace Infrastructure.Data.Repositories;

public class AttachmentRepository
    : RepositoryBase<Attachment>, IAttachmentRepository
{
    private const string _filesPath = "files";

    public AttachmentRepository(MongoDbContext context) : base(context)
    {
        Directory.CreateDirectory(_filesPath);
    }
    
    public new async Task AddAsync(Attachment attachment, CancellationToken cancellationToken)
    {
        var filePath = Path.Combine(_filesPath, attachment.Id + Path.GetExtension(attachment.File.FileName));

        await _dbSet.AddAsync(attachment, cancellationToken);
        
        if (attachment.File == null || attachment.File.Length == 0)
        {
            throw new FileLoadException("Invalid file");
        }

        await using var stream = new FileStream(filePath, FileMode.Create);
        
        await attachment.File.CopyToAsync(stream, cancellationToken);
        
        attachment.FilePath = filePath;
    }
}