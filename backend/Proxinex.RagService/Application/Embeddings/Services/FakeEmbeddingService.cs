using Proxinex.RagService.Application.Embeddings.Interfaces;

namespace Proxinex.RagService.Application.Embeddings.Services;

public class FakeEmbeddingService
    : IEmbeddingService
{
    public Task<float[]> GenerateEmbeddingAsync(
        string text)
    {
        var random =
            new Random();

        var vector =
            Enumerable.Range(0, 384)
                .Select(_ =>
                    (float)random.NextDouble())
                .ToArray();

        return Task.FromResult(vector);
    }
}