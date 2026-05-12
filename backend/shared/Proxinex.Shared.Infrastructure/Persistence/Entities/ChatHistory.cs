namespace Proxinex.Shared.Infrastructure.Persistence.Entities;

public class ChatHistory // for AI audit trail
{
    public Guid Id { get; set; }

    public string ConversationId { get; set; } = default!;

    public string Prompt { get; set; } = default!;

    public string Response { get; set; } = default!;

    public string Model { get; set; } = default!;

    public string Provider { get; set; } = default!;

    public long LatencyMs { get; set; }

    public string TraceId { get; set; } = default!;

    public DateTime Timestamp { get; set; }

    public int PromptTokens { get; set; }

    public int CompletionTokens { get; set; }

    public int TotalTokens { get; set; }

    public decimal EstimatedCost { get; set; }
}