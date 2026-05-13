namespace Proxinex.Shared.Infrastructure.Persistence.Entities;

public class DocumentChunk
{
    public Guid Id { get; set; }

    public string DocumentName { get; set; } = default!;

    public string Content { get; set; } = default!;

    public string EmbeddingId { get; set; } = default!;

    public int ChunkIndex { get; set; }

    public DateTime CreatedAt { get; set; }
}