namespace Proxinex.Shared.Contracts.Chat;

public class ChatResponse
{
    public string Response { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public string Provider { get; set; } = "Ollama";

    public string ConversationId { get; set; } = string.Empty;

    public double Temperature { get; set; }

    public bool Stream { get; set; }

    public DateTime Timestamp { get; set; }

    public Guid TraceId { get; set; }
    public long LatencyMs { get; set; }
}