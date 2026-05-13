namespace Proxinex.RagService.Application.Retrieval.Interfaces;

public interface IVectorStoreService
{
    Task StoreAsync(
        string id,
        string content,
        float[] embedding);

    Task<List<string>> SearchAsync(
        float[] embedding,
        int topK = 5);
}