using Microsoft.AspNetCore.Mvc;
using Proxinex.RagService.Application.Ingestion.Interfaces;
using Proxinex.RagService.Application.Embeddings.Interfaces;
using Proxinex.RagService.Application.Retrieval.Interfaces;
using Proxinex.Shared.Contracts.Rag;

namespace Proxinex.RagService.Controllers;

[ApiController]
[Route("api/rag")]
public class RagController : ControllerBase
{
    private readonly IDocumentIngestionService _ingestionService;

    private readonly IEmbeddingService _embeddingService;

    private readonly IVectorStoreService _vectorStore;

    public RagController(
        IDocumentIngestionService ingestionService,
        IEmbeddingService embeddingService,
        IVectorStoreService vectorStore)
    {
        _ingestionService = ingestionService;

        _embeddingService = embeddingService;

        _vectorStore = vectorStore;
    }

    [HttpPost("ingest")]
    public async Task<IActionResult> Ingest(
        [FromBody] IngestRequest request)
    {
        await _ingestionService.IngestAsync(
            request.DocumentName,
            request.Content);

        return Ok(new
        {
            message = "Document ingested successfully"
        });
    }

    [HttpPost("search")]
public async Task<IActionResult> Search(
    [FromBody] SearchRequest request)
{
    var embedding =
        await _embeddingService
            .GenerateEmbeddingAsync(
                request.Query);

    var results =
        await _vectorStore.SearchAsync(
            embedding);

    return Ok(new RagSearchResponse
    {
        Chunks = results,

        RetrievedCount = results.Count,

        Timestamp = DateTime.UtcNow
    });
}
public class IngestRequest
{
    public string DocumentName { get; set; }
        = default!;

    public string Content { get; set; }
        = default!;
}

public class SearchRequest
{
    public string Query { get; set; }
        = default!;
}
}