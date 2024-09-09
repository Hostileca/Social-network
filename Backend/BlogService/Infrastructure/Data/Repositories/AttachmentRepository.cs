using Application.Repositories;
using Domain.Entities;
using System.Web;

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
        var extension = Path.GetExtension(attachment.File.FileName);
        var fileName = attachment.Id + extension;
        var filePath = Path.Combine(_filesPath, fileName);
        attachment.ContentType = MimeTypes.GetMimeType(fileName);
        
        if (attachment.ContentType == MimeTypes.FallbackMimeType)
        {
            throw new FileLoadException("Invalid file type");
        }
        
        if (attachment.File == null || attachment.File.Length == 0)
        {
            throw new FileLoadException("Invalid file");
        }
        
        await _dbSet.AddAsync(attachment, cancellationToken);

        await using var stream = new FileStream(filePath, FileMode.Create);
        
        await attachment.File.CopyToAsync(stream, cancellationToken);
        
        attachment.FilePath = filePath;
    }

    public async Task<byte[]> LoadAsync(string filePath, CancellationToken cancellationToken)
    {
        byte[] fileBytes = await File.ReadAllBytesAsync(filePath, cancellationToken);

        return fileBytes;
    }
}