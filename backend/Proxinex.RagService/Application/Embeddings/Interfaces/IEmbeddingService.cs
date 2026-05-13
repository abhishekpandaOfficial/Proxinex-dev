namespace Proxinex.RagService.Application.Embeddings.Interfaces;

public interface IEmbeddingService
{
    Task<float[]> GenerateEmbeddingAsync(
        string text);
}