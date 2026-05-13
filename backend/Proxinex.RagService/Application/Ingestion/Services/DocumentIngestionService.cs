using Proxinex.RagService.Application.Chunking.Interfaces;
using Proxinex.RagService.Application.Embeddings.Interfaces;
using Proxinex.RagService.Application.Ingestion.Interfaces;
using Proxinex.RagService.Application.Retrieval.Interfaces;

namespace Proxinex.RagService.Application.Ingestion.Services;

public class DocumentIngestionService
    : IDocumentIngestionService
{
    private readonly ITextChunkingService _chunkingService;

    private readonly IEmbeddingService _embeddingService;

    private readonly IVectorStoreService _vectorStore;

    public DocumentIngestionService(
        ITextChunkingService chunkingService,
        IEmbeddingService embeddingService,
        IVectorStoreService vectorStore)
    {
        _chunkingService = chunkingService;

        _embeddingService = embeddingService;

        _vectorStore = vectorStore;
    }

    public async Task IngestAsync(
        string documentName,
        string content)
    {
        var chunks =
            _chunkingService.Chunk(content);

        foreach (var chunk in chunks)
        {
            var embedding =
                await _embeddingService
                    .GenerateEmbeddingAsync(chunk);

            await _vectorStore.StoreAsync(
                Guid.NewGuid().ToString(),
                chunk,
                embedding);
        }
    }
}