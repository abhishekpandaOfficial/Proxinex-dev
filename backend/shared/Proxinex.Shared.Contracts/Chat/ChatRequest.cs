namespace Proxinex.Shared.Contracts.Chat;

public class ChatRequest
{
    public string Prompt { get; set; } = string.Empty; // user input

    public string? ConversationId { get; set; } // memory/threading

    public string? Model { get; set; } // dynamic routing
 
    public string? SystemPrompt { get; set; } // AI behavior

    public double Temperature { get; set; } = 0.7; // creativity control

    public bool Stream { get; set; } = false; // realtime UX
} 