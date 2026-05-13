using Qdrant.Client;
using Qdrant.Client.Grpc;
using Proxinex.RagService.Application.Retrieval.Interfaces;

namespace Proxinex.RagService.Application.Retrieval.Services;

public class QdrantVectorStoreService
    : IVectorStoreService
{
    private readonly QdrantClient _client;

    private const string CollectionName =
        "proxinex-knowledge";

    public QdrantVectorStoreService()
    {
        _client = new QdrantClient(
            host: "localhost",
            port: 6334);
    }

    public async Task StoreAsync(
        string id,
        string content,
        float[] embedding)
    {
        // =========================
        // CHECK COLLECTION
        // =========================

        var collections =
            await _client.ListCollectionsAsync();

        if (!collections.Any(
            x => x == CollectionName))
        {
            await _client.CreateCollectionAsync(
                collectionName: CollectionName,
                vectorsConfig: new VectorParams
                {
                    Size = (ulong)embedding.Length,
                    Distance = Distance.Cosine
                });
        }

        // =========================
        // CREATE POINT
        // =========================

        var point = new PointStruct
        {
            Id = Guid.NewGuid(),

            Vectors = embedding
        };

        // =========================
        // ADD PAYLOAD
        // =========================

        point.Payload.Add(
            "content",
            content);

        point.Payload.Add(
            "documentId",
            id);

        point.Payload.Add(
            "createdAt",
            DateTime.UtcNow.ToString("O"));

        // =========================
        // UPSERT TO QDRANT
        // =========================

        await _client.UpsertAsync(
            collectionName: CollectionName,
            points: new[] { point });
    }

    public async Task<List<string>> SearchAsync(
        float[] embedding,
        int topK = 5)
    {
        // =========================
        // SEARCH QDRANT
        // =========================

        var results =
            await _client.SearchAsync(
                collectionName: CollectionName,
                vector: embedding,
                limit: (ulong)topK);

        // =========================
        // RETURN RESULTS
        // =========================

        return results
            .Where(x =>
                x.Payload.ContainsKey("content"))
            .Select(x =>
                x.Payload["content"].StringValue)
            .ToList();
    }
}