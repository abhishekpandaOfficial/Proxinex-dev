namespace Proxinex.Shared.Contracts.Rag;

public class RagSearchResponse
{
    public List<string> Chunks { get; set; }
        = new();

    public int RetrievedCount { get; set; }

    public DateTime Timestamp { get; set; }
}